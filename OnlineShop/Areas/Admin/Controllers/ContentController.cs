using Model.DAO;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.IO;
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
            string image = string.Empty;
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    //var fileName = Path.GetFileName(file.FileName);
                    string newFileName = Guid.NewGuid().ToString();
                    string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                    string filename = newFileName + fileExtension;

                    var path = Path.Combine(Server.MapPath("~/Upload/Images/"), filename);
                    file.SaveAs(path);
                    image = "/Upload/Images/" + filename;
                }

            }
            if(ModelState.IsValid)
            {
                
                var contentDAO = new ContentDAO();
                obj.CreatedDate = DateTime.Now;
                obj.Image = image;
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
                else
                {
                    ModelState.AddModelError("", "Thêm mới tin tức không thành công.");
                    return View(obj);
                }
            }
            else
            {
                return View(obj);
            }
             
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new ContentDAO().GetContenById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Content obj)
        {
            if(ModelState.IsValid)
            {
                var userLogin = (UserLogin) Session[CommonContants.USER_SESSION];
                obj.ModifiedBy = userLogin.UserName;
                obj.ModifiedDate = DateTime.Now;
                 
                var flag = new ContentDAO().Update(obj);
                if (flag)
                {
                    SetAlert("Cập nhật tin tức thành công !", "SUCCESS");
                    TempData["CheckMessage"] = "CHECK";
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật tin tức không thành công.");
                    return View(obj);
                }
                   

            }
            else
            {
                return View(obj);
            }
             
        }

        public ActionResult Delete(long id)
        {
            var flag = new ContentDAO().Delete(id);
            if(flag)
            {
                SetAlert("Xóa tin tức thành công !", "SUCCESS");
                TempData["CheckMessage"] = "CHECK";
                return RedirectToAction("Index", "Content");
            }
            else
            {
                SetAlert("Xóa tin tức không thành công !", "ERROR");
                TempData["CheckMessage"] = "CHECK";
                return RedirectToAction("Index", "Content");
            }
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ContentDAO().ChangeStatus(id);

            return new JsonResult { Data = new { status = result } };
        }
    }
}