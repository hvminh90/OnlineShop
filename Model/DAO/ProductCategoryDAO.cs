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
    }
}
