using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDAO().GetActiveContact();
            return View(model);
        }

        [HttpPost]
        public JsonResult InsertFeedback(string name, string phone, string email, string address, string content)
        {
            var feedbackDAO = new FeedbackDAO();
            var feedBack = new FeedBack()
            {
                Name = name,
                Phone = phone,
                Email = email,
                Address = address,
                Content = content,
                CreatedDate = DateTime.Now,
                Status = true
            };
            var id = feedbackDAO.InsertFeedBack(feedBack);
            bool status = false;
            if (id > 0) status = true;
            return new JsonResult { Data = new { Status = status } };
        }
    }
}