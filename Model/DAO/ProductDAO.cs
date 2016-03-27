using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProductDAO
    {
        OnlineShopDbContext db = null;
        public ProductDAO()
       {
           db = new OnlineShopDbContext();

       }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedBy).Take(top).ToList();
        }
        public List<Product> ListFeatureProduct(int top)
        {
            //return db.Products.Where(p=>p.TopHot != null && p.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedBy).Take(top).ToList();
            return db.Products.OrderByDescending(x => x.CreatedBy).Take(top).ToList();
        }
        public List<Product> ListRelatedProduct(int productId)
        {
            var product = db.Products.SingleOrDefault(p => p.ID == productId);
            return db.Products.Where(p => p.ID != productId && p.CategoryID == product.CategoryID).ToList();
        }
        public Product ViewDetail(int productId)
        {
            return db.Products.SingleOrDefault(x => x.ID == productId);
        }

        public List<Product> ListByCategoryID(int categoryID,ref int total, int page,int pageSize)
        {
            total = db.Products.Where(p => p.CategoryID == categoryID && p.Status == true).Count();
            var mode = db.Products.Where(p => p.CategoryID == categoryID && p.Status == true).OrderByDescending(p=>p.CreatedDate).Skip((page-1)*pageSize).Take(pageSize).ToList();
            return mode;
        }
        public List<string> ListName(string q)
        {
            return db.Products.Where(p => p.Name.Contains(q)).Select(p => p.Name).ToList();
        }

        public List<Product> Search(string keyword, ref int total, int page, int pageSize)
        {
            total = db.Products.Where(p => p.Name.Contains(keyword) && p.Status == true).Count();
            var mode = db.Products.Where(p => p.Name.Contains(keyword) && p.Status == true).OrderByDescending(p => p.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return mode;
        }
        public List<Product> ListProductByCategoryId(int top,long categoryId)
        {
            return db.Products.Where(p => p.CategoryID == categoryId && p.Status == true).OrderByDescending(x => x.CreatedBy).Take(top).ToList();
        }

        public List<Product> GetAllProduct()
        {
            return db.Products.ToList();
        }

        public long Insert(Product model)
        {
            db.Products.Add(model);
            db.SaveChanges();
            return model.ID;
        }

        public bool Update (Product model)
        {
            try
            {
                var entity = db.Products.Where(p => p.ID == model.ID).FirstOrDefault();

                entity.ID = model.ID;
                entity.Name = model.Name;
                entity.Code = model.Code;
                entity.MetaTitle = model.MetaTitle;
                entity.Description = model.Description;
                entity.Image = model.Image == null ? entity.Image : model.Image;
                entity.MoreImages = model.MoreImages;
                entity.Price = model.Price;
                entity.PromotionPrice = model.PromotionPrice;
                entity.Quantity = model.Quantity;
                entity.CategoryID = model.CategoryID;
                entity.Detail = model.Detail;
                entity.ModifiedBy = model.ModifiedBy;
                entity.ModifiedDate = model.ModifiedDate;
                entity.Status = model.Status;
                entity.TopHot = model.TopHot;

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }

    
}
