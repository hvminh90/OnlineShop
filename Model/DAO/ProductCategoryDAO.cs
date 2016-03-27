using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProductCategoryDAO
    {
        OnlineShopDbContext db = null;
        public ProductCategoryDAO()
        {
            db = new OnlineShopDbContext();

        }

        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(p => p.Status == true).OrderBy(p => p.DisplayOrder).ToList();
        }

        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.SingleOrDefault(p => p.ID == id);
        }
        public List<ProductCategory> GetListAll()
        {
            return db.ProductCategories.ToList();
        }
        public long Insert(ProductCategory model)
        {
            db.ProductCategories.Add(model);
            db.SaveChanges();
            return model.ID;
        }
        //public bool Delete(long id)
        //{
        //    var obj = db.ProductCategories.Where(p => p.ID == id).FirstOrDefault();
        //    db.ProductCategories.Remove(obj);
        //    return true;
        //}

        public bool Update(ProductCategory model)
        {
            try
            {
                var obj = db.ProductCategories.Where(p => p.ID == model.ID).FirstOrDefault();
                obj.Name = model.Name;
                obj.MetaTitle = model.MetaTitle;
                obj.ParentID = model.ParentID;
                obj.DisplayOrder = model.DisplayOrder;
                obj.SeoTitle = model.SeoTitle;
                obj.ModifiedBy = model.ModifiedBy;
                obj.ModifiedDate = model.ModifiedDate;
                obj.MetaKeywords = model.MetaKeywords;
                obj.MetaDescriptions = model.MetaDescriptions;
                obj.Status = model.Status;

                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<ProductCategory> GetListCategoryInProduct()
        {
            var lst = (from product in db.Products 
                       join category in db.ProductCategories on product.CategoryID equals category.ID
                       select   category 
                       );
            return lst.Distinct().ToList() ;
        }

        public bool Delete(long id)
        {
            try
            {
                var model = db.ProductCategories.Where(p => p.ID == id).FirstOrDefault();
                var listModel = db.ProductCategories.Where(p => p.ParentID == id).ToList();
                if(listModel.Any())
                {
                    db.ProductCategories.RemoveRange(listModel);
                }
                db.ProductCategories.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }
    }
}
