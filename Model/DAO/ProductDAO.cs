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
            return db.Products.Where(p=>p.TopHot != null && p.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedBy).Take(top).ToList();
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
    }
}
