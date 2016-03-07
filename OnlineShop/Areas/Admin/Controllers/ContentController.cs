using Model.DAO;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(int? page, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
                ViewBag.SearchString = searchString;
            var dao = new ContentDAO();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var model = dao.ListAllPaging(pageNumber, pageSize, searchString);
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
        public List<Category> ListCategory ()
        {
            var lstCateory = new CategoryDAO().GetAllCategory();
            return lstCateory;
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Content obj)
        {
            if(ModelState.IsValid)
            {
                var contentDAO = new ContentDAO();
                obj.CreatedDate = DateTime.Now;
                var userLogin = (UserLogin) Session[CommonContants.USER_SESSION];
                obj.CreatedBy = userLogin.UserName;
                long id = contentDAO.InsertContent(obj);
                obj.Language = "vi";
                if (id > 0)
                {
                    SetAlert("Thêm mới tin tức thành công !", "SUCCESS");
                    TempData["CheckMessage"] = "CHECK";
                    return RedirectToAction("Index", "Content");
                }
                else ModelState.AddModelError("", "Thêm mới tin tức không thành công.");
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new ContentDAO().GetContenById(id);
            return View(model);
        }
    }
}