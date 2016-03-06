using Model.DAO;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace OnlineShop.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
           
            var productDAO = new ProductDAO();
            var slideDao = new SlideDAO();
            ViewBag.Slides = slideDao.ListAll();
            ViewBag.ListNewProduct = productDAO.ListNewProduct(4);
            ViewBag.ListFeatureProduct = productDAO.ListFeatureProduct(4);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration=3600 * 24)]
        public ActionResult MainMenu()
        {
            var dao = new MenuDAO();
            var model = dao.ListByGroupId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult TopMenu()
        {
            var dao = new MenuDAO();
            var model = dao.ListByGroupId(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult Footer()
        {
            var dao = new FooterDAO();
            var model = dao.GetFooter();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Slide()
        {
            var dao = new SlideDAO();
            var model = dao.ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session["CartSession"];
            var list = new List<CartItem>();
            int totalCount = 0;
            if (cart != null)
            {
                list = (List<CartItem>)Session["CartSession"];
                totalCount = list.Count();
            }

            int total = 0;
            foreach (var item in list)
            {
                total += (int)(item.Quantity * item.Product.Price);
            }
            ViewBag.TotalPrice = total;
            ViewBag.TotalCount = totalCount;
            return PartialView();
        }


    }
}