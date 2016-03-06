using BotDetect.Web.UI.Mvc;
using Facebook;
using Model.DAO;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel objLogin, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(objLogin.UserName, Encryptor.MD5Hash(objLogin.Password));
                if (result == 1)
                {
                    FormsAuthentication.SetAuthCookie(objLogin.UserName, objLogin.Remember);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                    return View();
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                    return View();
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                    return View();
                }
                else
                    ModelState.AddModelError("", "Đăng nhập không thành công");
            }

           
            return View();



        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [CaptchaValidation("CaptchaCode", "SampleCaptcha", "Captcha không đúng.!")]
        public ActionResult Register(RegisterModel obj)
        {
            if(ModelState.IsValid)
            {
                var userDAO = new UserDAO();
                var encryptedMd5Pas = Encryptor.MD5Hash(obj.Password);
               

                if(userDAO.CheckUsername(obj.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
                    return View(obj);
                }
                if (userDAO.CheckEmail(obj.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại.");
                    return View(obj);
                }
                var user = new User()
                {
                    UserName = obj.UserName,
                    Password = encryptedMd5Pas,
                    Name = obj.Name,
                    Address = obj.Address,
                    Phone = obj.Phone,
                    CreatedBy ="admin",
                    CreatedDate = DateTime.Now,
                    Status = true
                };

                var id = userDAO.Insert(user);
                if(id > 0)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng ký không thành công.");
                    return View(obj);
                }

            }
            return View(obj);
        }

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new 
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                respone_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accesstoken = result.access_token;
            if(!string.IsNullOrEmpty(accesstoken))
            {
                fb.AccessToken = accesstoken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string first_name = me.first_name;
                string middle_name = me.middle_name;
                string last_name = me.last_name;
                string email = me.email;

                var user = new User()
                {
                    UserName = email,
                    Email = email,
                    CreatedDate = DateTime.Now,
                    Status = true,
                    Name = first_name + " " + middle_name + " " + last_name
                };

                var id = new UserDAO().InsertFacebook(user);
                if (id > 0)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                }
                    
            }
            return Redirect("/");
        }
    }
}