using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
                lang = langCookie.Value;
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = SiteLanguage.GetLanguageDefault();
                }

            }
            new SiteLanguage().SetLanguage(lang);
            //TempData["lang"] = lang;
            return base.BeginExecuteCore(callback, state);

        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            
            base.Initialize(requestContext);
        }
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonContants.USER_SESSION];
            if(session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller="Login",
                    action="Index",
                    Area="Admin"
                }));

                
            }
            base.OnActionExecuting(filterContext);
        }

        public void SetAlert(string strAlert, string type)
        {
            if (!string.IsNullOrEmpty(strAlert))
                TempData["Message"] = strAlert;
            else TempData["Message"] = string.Empty;

            if (type == "ERROR")
                TempData["MessageAlert"] = "alert-danger";
            else if(type=="SUCCESS")
                TempData["MessageAlert"] = "alert-success";
            else if(type=="WARNING")
                TempData["MessageAlert"] = "alert-warning";
            else TempData["MessageAlert"] = "alert-info";
        }
        public ActionResult ChangeLanguage(string lang)
        {
            new SiteLanguage().SetLanguage(lang);
            //TempData["lang"] = lang;
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("Index", "Home");
        }
    }
}