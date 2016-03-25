using Model.DAO;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
     [AuthorizeClient]
    public class ContentController : Controller
    {
         int pageSize = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["PageSizeContent"]) ? int.Parse(ConfigurationManager.AppSettings["PageSizeContent"]) : 3;
        // GET: Content
       
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            //int pageSize = 1;
            int total = 0;

            int maxPage = 5;
            int totalPage = 0;

            var model = new ContentDAO().ListPaging(ref total, pageNumber, pageSize);

            totalPage = (int)Math.Ceiling(((double)total / pageSize));

            ViewBag.Page = pageNumber;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;

            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = pageNumber + 1;
            ViewBag.Pre = pageNumber - 1;


            return View(model);
        }
        [AuthorizeClient(Roles = "admin")]
        public ActionResult Detail(long id)
        {
            var contentDAO = new ContentDAO();
            var model = contentDAO.GetContenById(id);
            var lstTag = contentDAO.GetAllTagByContenId(id);
            ViewBag.ListTag = lstTag;
            return View(model);
        }

        public ActionResult Tags(string tagId,int? page)
        {
            int pageNumber = (page ?? 1);
            //int pageSize = 1;
            int total = 0;

            int maxPage = 5;
            int totalPage = 0;
            var contenDAO = new ContentDAO();
            var model = contenDAO.ListPagingTag(tagId, ref total, pageNumber, pageSize);

            ViewBag.Tag = contenDAO.GetTagByTagId(tagId);

            totalPage = (int)Math.Ceiling(((double)total / pageSize));

            ViewBag.Page = pageNumber;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.TagID = tagId;

            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = pageNumber + 1;
            ViewBag.Pre = pageNumber - 1;


            return View(model);
        }
    }
}