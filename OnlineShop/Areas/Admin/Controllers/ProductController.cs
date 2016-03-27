using Common;
using Model.DAO;
using Model.EF;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using Common;
using OnlineShop.Common;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
       
        // GET: Admin/Product
        public ActionResult Index(int? page, string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
                ViewBag.SearchString = SearchString;
           
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //ViewBag.Page = (pageNumber - 1) * pageSize;
            if (pageNumber == 1)
                ViewBag.Index = 1;
            else ViewBag.Index = pageSize * (pageNumber - 1) + 1;

            var lstProduct = new ProductDAO().GetAllProduct();
            var lstProductCategory = new ProductCategoryDAO().GetListAll();
            var lstProductModel = lstProduct.Select(p => new ProductModel
            {
                product = p,
                CategoryName = lstProductCategory.Where(x => x.ID == p.CategoryID).FirstOrDefault() != null ?lstProductCategory.Where(x => x.ID == p.CategoryID).FirstOrDefault().Name : ""
            }).ToList();

            lstProductModel.OrderByDescending(p => p.product.CreatedDate);
            if(!string.IsNullOrEmpty(SearchString))
                lstProductModel = lstProductModel.Where(x => StringHelper.RemoveSign4VietnameseString(x.product.Name).Trim().ToLower().Contains(StringHelper.RemoveSign4VietnameseString(SearchString).Trim().ToLower())).ToList();
            
            return View(lstProductModel.ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product model, HttpPostedFileBase file, List<HttpPostedFileBase> fileUpload, string Price)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    List<ImageProduct> lstImageProduct = new List<ImageProduct>();

                    string imagePath = string.Empty;
                    string moreImagePath = string.Empty;
                    string pathUpload = "images/product/";
                    string pathUploadDate = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();

                    if (!Directory.Exists(Server.MapPath("~/" + pathUpload)))
                        Directory.CreateDirectory(Server.MapPath("~/" + pathUpload));
                    if (!Directory.Exists(Server.MapPath("~/" + pathUpload + pathUploadDate + "/")))
                        Directory.CreateDirectory(Server.MapPath("~/" + pathUpload + pathUploadDate + "/"));
                    //Upload file Hình ảnh chính
                    if(file != null && file.ContentLength > 0)
                    {
                        string gui = Guid.NewGuid().ToString();   
                        string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                        string filename = gui + fileExtension;
                       
                        var path = Path.Combine(Server.MapPath("~/" + pathUpload + pathUploadDate + "/"), filename);
                        file.SaveAs(path);
                        imagePath = "/" + pathUpload + pathUploadDate + "/" + filename;
                    }

                    //Upload file hình ảnh phụ
                    if(fileUpload.Count > 0)
                    {
                        foreach (HttpPostedFileBase item in fileUpload)
                        {
                            if (item != null && item.ContentLength > 0)
                            {
                                string gui_item = Guid.NewGuid().ToString();   
                                string fileExtension = System.IO.Path.GetExtension(item.FileName).ToLower();
                                string filename = gui_item + fileExtension;

                                var path = Path.Combine(Server.MapPath("~/" + pathUpload + pathUploadDate + "/"), filename);
                                item.SaveAs(path);
                                moreImagePath = "/" + pathUpload + pathUploadDate + "/" + filename;

                                var objImageProduct = new ImageProduct
                                {
                                    FileID = filename,
                                    ImagePath = moreImagePath
                                };
                                lstImageProduct.Add(objImageProduct);
                            }
                        }
                    }
                    //----------------------------------------------------------------
                    //Insert thông tin sản phẩm
                    var userLogin = (UserLogin)Session[CommonContants.USER_SESSION];
                   
                    model.MetaTitle = StringHelper.ToUnsignString(model.Name);
                    model.Image = !string.IsNullOrEmpty(imagePath) ? imagePath : null;
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = userLogin.UserName;
                    model.ModifiedDate = DateTime.Now;
                    model.ModifiedBy = userLogin.UserName;
                    model.ViewCount = 0;

                    JavaScriptSerializer objJSSerializer = new JavaScriptSerializer();
                    string JsonMoreImage = string.Empty;
                    if(lstImageProduct.Count > 0)
                    {
                        JsonMoreImage = objJSSerializer.Serialize(lstImageProduct);

                    }
                    model.MoreImages = JsonMoreImage;

                    var id = new ProductDAO().Insert(model);
                    if(id > 0)
                    {
                         SetAlert("Thêm mới sản phẩm thành công !", "SUCCESS");
                        TempData["CheckMessage"] = "CHECK";
                        return RedirectToAction("Index", "Product");
                
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm mới sản phẩm không thành công.");
                        return View(model);
                    }

                }
                model.Price = 100000;
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Thêm mới sản phẩm không thành công.");
                return View(model);
                //throw;
            }
           

        }

        [HttpGet]
        public ActionResult Edit(long id)
        {

            var objProductModelEdit = GetProductEdit(id);
            return View(objProductModelEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ProductModelEdit model, HttpPostedFileBase file, List<HttpPostedFileBase> fileUpload)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    if (string.IsNullOrEmpty(model.Product.Image) && file == null)
                    {
                        ModelState.AddModelError("", "Chưa chọn image cho sản phẩm");
                        var objProductModelEdit = GetProductEdit(model.Product.ID);
                        objProductModelEdit.Product.Image = string.Empty;
                        return View(objProductModelEdit);
                    }

                    JavaScriptSerializer objJSSerializer = new JavaScriptSerializer();
                    string imagePath = string.Empty;
                    string moreImagePath = string.Empty;

                    string pathUpload = "images/product/";
                    string pathUploadDate = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();

                    if (!Directory.Exists(Server.MapPath("~/" + pathUpload)))
                        Directory.CreateDirectory(Server.MapPath("~/" + pathUpload));
                    if (!Directory.Exists(Server.MapPath("~/" + pathUpload + pathUploadDate + "/")))
                        Directory.CreateDirectory(Server.MapPath("~/" + pathUpload + pathUploadDate + "/"));
                    //Nếu mà có upload lại ảnh avatar sản phẩm thì mới save
                    if (file != null && file.ContentLength > 0)
                    {
                        string gui = Guid.NewGuid().ToString();
                        string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                        string filename = gui + fileExtension;

                        var path = Path.Combine(Server.MapPath("~/" + pathUpload + pathUploadDate + "/"), filename);
                        file.SaveAs(path);
                        imagePath = "/" + pathUpload + pathUploadDate + "/" + filename;
                    }

                    //Update nhiều ảnh của sản phẩm lại
                    List<ImageProduct> lstImageProductOld = new List<ImageProduct>();
                    List<ImageProduct> lstImageProductNew = new List<ImageProduct>();
                    if (!string.IsNullOrEmpty(model.StringImageProduct))
                        lstImageProductOld = objJSSerializer.Deserialize<List<ImageProduct>>(model.StringImageProduct);
                    if (fileUpload.Count > 0)
                    {
                        foreach (HttpPostedFileBase item in fileUpload)
                        {
                            if (item != null && item.ContentLength > 0)
                            {
                                string gui_item = Guid.NewGuid().ToString();
                                string fileExtension = System.IO.Path.GetExtension(item.FileName).ToLower();
                                string filename = gui_item + fileExtension;

                                var path = Path.Combine(Server.MapPath("~/" + pathUpload + pathUploadDate + "/"), filename);
                                item.SaveAs(path);
                                moreImagePath = "/" + pathUpload + pathUploadDate + "/" + filename;

                                var objImageProduct = new ImageProduct
                                {
                                    FileID = filename,
                                    ImagePath = moreImagePath
                                };
                                lstImageProductNew.Add(objImageProduct);
                            }
                        }
                    }
                    if(lstImageProductNew.Count > 0)
                        lstImageProductOld.AddRange(lstImageProductNew);

                    string moreImage = string.Empty;
                    if(lstImageProductOld.Count > 0)
                        moreImage = objJSSerializer.Serialize(lstImageProductOld);

                    var userLogin = (UserLogin)Session[CommonContants.USER_SESSION];
                    var entity = new Product();

                    entity.ID = model.Product.ID;
                    entity.Name = model.Product.Name;
                    entity.Code = model.Product.Code;
                    entity.MetaTitle = StringHelper.ToUnsignString(model.Product.Name);
                    entity.Description = model.Product.Description;
                    entity.Image = !string.IsNullOrEmpty(imagePath) ? imagePath : model.Product.Image;
                    entity.MoreImages = moreImage;
                    entity.Price = model.Product.Price;
                    entity.PromotionPrice = model.Product.PromotionPrice;
                    entity.Quantity = model.Product.Quantity;
                    entity.CategoryID = model.Product.CategoryID;
                    entity.Detail = model.Product.Detail;
                    entity.ModifiedBy = userLogin.UserName;
                    entity.ModifiedDate = DateTime.Now;
                    entity.Status = model.Product.Status;
                    entity.TopHot = model.Product.TopHot;

                    var result = new ProductDAO().Update(entity);
                    if(result)
                    {
                        SetAlert("Cập nhật sản phẩm thành công !", "SUCCESS");
                        TempData["CheckMessage"] = "CHECK";
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Lỗi cập nhật thông tin sản phẩm");
                        var objProductModelEdit = GetProductEdit(model.Product.ID);
                        return View(objProductModelEdit);
                    }


                }
                var obj = GetProductEdit(model.Product.ID);
                return View(obj);
            }
            catch (Exception)
            {
                ModelState.AddModelError("","Lỗi cập nhật thông tin sản phẩm");
                var objProductModelEdit = GetProductEdit(model.Product.ID);
                return View(objProductModelEdit);
                //throw;
            }
            //return View(model);
        }

        public ActionResult Delete(long id)
        {

            return RedirectToAction("Index");
        }

        [NonAction]
        public ProductModelEdit GetProductEdit(long id)
        {
            var product = new ProductDAO().ViewDetail((int)id);
            List<ImageProduct> lstImageProduct = new List<ImageProduct>();
            var objProductModelEdit = new ProductModelEdit();
            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer();
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.MoreImages))
                    lstImageProduct = objJSSerializer.Deserialize<List<ImageProduct>>(product.MoreImages);
                objProductModelEdit.Product = product;
                objProductModelEdit.ListImageProduct = lstImageProduct;
                objProductModelEdit.StringImageProduct = string.Empty;
            }
            return objProductModelEdit;
        }
    }
}