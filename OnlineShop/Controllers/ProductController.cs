using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ProductCategory()
        {
            var dao = new ProductCategoryDAO();
            var model = dao.ListAll();
            return PartialView(model);
        }

        //[Route("san-pham/{metatitle}-{categoryID}")]
        public ActionResult Category(int categoryID, int page = 1)
        {
            var productDao = new ProductDAO();
            var productCategoryDao = new ProductCategoryDAO();
            var total = 0;
            var pageSize = 8;
            var model = productDao.ListByCategoryID(categoryID,ref total, page, pageSize);
            var category = productCategoryDao.ViewDetail(categoryID);


            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)total / pageSize);
            ViewBag.TotalPage = totalPage;
            ViewBag.Page = page;
            ViewBag.MaxPage = maxPage;

            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            ViewBag.Category = category;



            return View(model);
        }
         [Route("chi-tiet/{metatitle}-{productId}")]
        public ActionResult Detail (int productId)
        {
            var productDao = new ProductDAO();
            var product = productDao.ViewDetail(productId);
            var productCategoryDao = new ProductCategoryDAO();
            var category = productCategoryDao.ViewDetail(product.CategoryID.Value);
            ViewBag.Category = category;
            ViewBag.RelatedProducts = productDao.ListRelatedProduct(productId);
            return View(product);
        }
    }
}