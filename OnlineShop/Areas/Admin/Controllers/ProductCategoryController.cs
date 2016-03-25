using Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using OnlineShop.Common;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/CategoryProduct
        public ActionResult Index(int? page, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
                ViewBag.SearchString = searchString;
            var dao = new ProductCategoryDAO();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.Page = (pageNumber - 1) * pageSize;

            IEnumerable<ProductCategory> lstProductCategory = dao.GetListAll();
            lstProductCategory = lstProductCategory.OrderByDescending(x => x.CreatedDate).ToList();

            if (!string.IsNullOrEmpty(searchString))
                lstProductCategory = lstProductCategory.Where(x => StringHelper.RemoveSign4VietnameseString(x.Name).Trim().ToLower().Contains(StringHelper.RemoveSign4VietnameseString(searchString).Trim().ToLower())).ToList();

            ViewBag.ListProductCategory = dao.GetListAll();

            return View(lstProductCategory.ToPagedList(pageNumber, pageSize));
            
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ProductCategory model)
        {
            if(ModelState.IsValid)
            {
                var userLogin = (UserLogin)Session[CommonContants.USER_SESSION];
                model.CreatedBy = userLogin.UserName;
                model.MetaTitle = StringHelper.ToUnsignString(model.Name);
                model.CreatedDate = DateTime.Now;

                var id = new ProductCategoryDAO().Insert(model);
                if(id > 0)
                {
                    SetAlert("Thêm mới danh mục sản phẩm thành công !", "SUCCESS");
                    TempData["CheckMessage"] = "CHECK";
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    SetAlert("Thêm mới danh mục sản phẩm lỗi !", "ERROR");
                    TempData["CheckMessage"] = "CHECK";
                    return RedirectToAction("Index", "ProductCategory");
                }
            }
            return PartialView("_Create",model);
        }

        [HttpGet]
        
        public ActionResult Delete(long id)
        {
            ViewBag.ProductCategoryID = id;
            return PartialView("_Delete");
        }
        //[HttpPost]
        //public ActionResult Delete(long id)
        //{

        //}

    }
}