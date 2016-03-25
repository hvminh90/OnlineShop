using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Common;
using OnlineShop.Common;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(int? page, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
                ViewBag.SearchString = searchString;
            var dao = new CategoryDAO();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
             ViewBag.Page = (pageNumber - 1) * pageSize;

            IEnumerable<Category> lstCategory = dao.GetAllCategory();
            lstCategory = lstCategory.OrderByDescending(x => x.CreatedDate).ToList();

            if (!string.IsNullOrEmpty(searchString))
                lstCategory = lstCategory.Where(x =>StringHelper.RemoveSign4VietnameseString(x.Name).Trim().ToLower().Contains(StringHelper.RemoveSign4VietnameseString(searchString).Trim().ToLower())).ToList();
           

            return View(lstCategory.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Category model )
         {
            if(ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                model.CreatedDate = DateTime.Now;
                var userLogin = (UserLogin)Session[CommonContants.USER_SESSION];
                model.CreatedBy = userLogin.UserName;
                model.MetaTitle = StringHelper.ToUnsignString(model.Name);

                long id = dao.Insert(model);
                if(id> 0)
                {
                    SetAlert("Thêm mới danh mục thành công !", "SUCCESS");
                    TempData["CheckMessage"] = "CHECK";
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Thêm mới danh mục lỗi !", "ERROR");
                    TempData["CheckMessage"] = "CHECK";
                    return RedirectToAction("Index", "Category");
                }
            }
             return PartialView("_Create",model);
        }

        public ActionResult Edit(long id)
        {
            var model = new CategoryDAO().GetCategory(id);
            return PartialView("_Edit", model);
        }
        [HttpGet]
        public ActionResult Delete(long id)
        {
            ViewBag.CategoryID = id;
            return PartialView("_Delete");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(long id)
        {
            var result = new CategoryDAO().Delete(id);
            if(result)
            {
                SetAlert("Xóa danh mục thành công !", "SUCCESS");
                TempData["CheckMessage"] = "CHECK";
                return RedirectToAction("Index", "Category");
            }
            else
            {
                SetAlert("Xóa danh mục lỗi !", "ERROR");
                TempData["CheckMessage"] = "CHECK";
                return RedirectToAction("Index", "Category");
            }
            //return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                //model.CreatedDate = DateTime.Now;
                var userLogin = (UserLogin)Session[CommonContants.USER_SESSION];
                //model.CreatedBy = userLogin.UserName;
                model.MetaTitle = StringHelper.ToUnsignString(model.Name);
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = userLogin.UserName;

                var result = dao.Edit(model);
                if (result)
                {
                    SetAlert("Cập nhật danh mục thành công !", "SUCCESS");
                    TempData["CheckMessage"] = "CHECK";
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Cập nhật danh mục lỗi !", "ERROR");
                    TempData["CheckMessage"] = "CHECK";
                    return RedirectToAction("Index", "Category");
                }
            }
            return PartialView("_Edit", model);
        }
    }
}