using Model.DAO;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        //[AuthorizeUser(Roles="MEMBER")]
        public ActionResult Index(int? page, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
                ViewBag.SearchString = searchString;
            var dao = new UserDAO();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var model = dao.ListAllPaging(pageNumber, pageSize,searchString);
            if (pageNumber == 1)
                ViewBag.Index = 1;
            else ViewBag.Index = pageSize * (pageNumber - 1) + 1;
            
            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDAO();
                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm mới thành công !", "SUCCESS");
                    TempData["CheckMessage"] = "CHECK";
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                    //SetAlert("Thêm mới không thành công !", "ERROR");
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new UserDAO();
            var user = dao.GetByID(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
              
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Cập nhật thành công !", "SUCCESS");
                    TempData["CheckMessage"] = "CHECK";
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("Index");
        }

        //[HttpPost]
        //public JsonResult Delete(int id)
        //{
        //    var dao = new UserDAO();
        //    string status = "";
        //    var result = dao.Delete(id);
        //    if (result) status = "TRUE";
        //    else
        //    {
        //        status = "FALSE";
        //    }
        //    return new JsonResult { Data = status };
        //}

        public ActionResult Delete(int id)
        {
            var dao = new UserDAO();
           
            var result = dao.Delete(id);
            if (result) return RedirectToAction("Index");
            else
            {
                ModelState.AddModelError("", "Lỗi xóa sản phẩm");
            }
            return View("Index");

        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var dao = new UserDAO();
            var result = dao.ChangeStatus(id);

            //return Json(new
            //{
            //    status = result
            //});
            return new JsonResult { Data = new { status = result } };
        }

    }
}