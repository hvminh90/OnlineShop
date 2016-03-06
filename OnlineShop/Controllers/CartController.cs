using Common;
using Model.DAO;
using Model.EF;
using Newtonsoft.Json;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        // GET: Cart
        private string CartSession = "CartSession";
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null) list = (List<CartItem>)Session[CartSession];
            int total = 0;
            foreach (var item in list)
            {
                total += (int)(item.Quantity * item.Product.Price);
            }
            ViewBag.TotalPrice = total;
            return View(list);
        }

        public ActionResult AddItem(long productId, int quantity = 1)
        {
            var product = new ProductDAO().ViewDetail((int)productId);
            var session = Session[CartSession];
            if (session != null)
            {
                var list = (List<CartItem>)Session[CartSession];
                if (list.Where(p => p.Product.ID == productId).Count() > 0)
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);


                Session[CartSession] = list;

            }

            return RedirectToAction("Index");
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return new JsonResult { Data = new { status = true } };
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {

            if (Session[CartSession] != null)
            {
                var list = (List<CartItem>)Session[CartSession];
                list.RemoveAll(x => x.Product.ID == id);
                Session[CartSession] = list;
            }
            return new JsonResult { Data = new { status = true } };
        }

        public JsonResult Update(string model)
        {
            var listCart = JsonConvert.DeserializeObject<List<CartItem>>(model);
            var listCartSession = (List<CartItem>)Session[CartSession];

            foreach (var item in listCartSession)
            {
                var jsonItem = listCart.Where(p => p.Product.ID == item.Product.ID).SingleOrDefault();
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = listCartSession;
            return new JsonResult { Data = new { status = true } };
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null) list = (List<CartItem>)Session[CartSession];
            int total = 0;
            foreach (var item in list)
            {
                total += (int)(item.Quantity * item.Product.Price);
            }
            ViewBag.TotalPrice = total;
            return View(list);

        }

        [HttpPost]
        public ActionResult Payment(string ShipName, string ShipMobile, string ShipAddress, string ShipEmail)
        {
            try
            {
                var cart = Session[CartSession];
                var list = new List<CartItem>();
                if (cart != null) list = (List<CartItem>)Session[CartSession];


                int total = 0;
                foreach (var item in list)
                {
                    total += (int)(item.Quantity * item.Product.Price);
                }

                var objOrder = new Order
                {
                    CustomerID = 1,
                    ShipName = ShipName,
                    ShipAddress = ShipAddress,
                    ShipEmail = ShipEmail,
                    ShipMobile = ShipMobile,
                    CreatedDate = DateTime.Now
                };

                var objOrderDAO = new OrderDAO();
                var objOrderDetailDAO = new OrderDetailDAO();
                long id = objOrderDAO.Insert(objOrder);

                foreach (var item in list)
                {
                    var objOrderDetail = new OrderDetail
                    {
                        OrderID = id,
                        ProductID = item.Product.ID,
                        Quantity = item.Quantity,
                        Price = item.Product.Price
                    };
                    objOrderDetailDAO.Insert(objOrderDetail);
                }

                Session[CartSession] = null;

                string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));
                content = content.Replace("{{CustomerName}}", ShipName);
                content = content.Replace("{{Phone}}", ShipMobile);
                content =  content.Replace("{{Email}}", ShipEmail);
                content = content.Replace("{{Address}}", ShipAddress);
                content = content.Replace("{{Total}}", total.ToString("N0"));

                new MailHelper().SendMail("hvbinh1990@gmail.com", "Đơn hàng từ online shop", content);
                new MailHelper().SendMail(ShipEmail, "Đơn hàng từ online shop", content);


                return Redirect("/thanh-toan-thanh-cong");
            }
            catch (Exception ex)
            {
                return Redirect("/loi-thanh-toan");
              
            }
        }

        [Route("thanh-toan-thanh-cong")]
        public ActionResult PaymentSuccess()
        {
            return View();
        }
    }
}