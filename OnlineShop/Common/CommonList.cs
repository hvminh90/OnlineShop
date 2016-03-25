using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Common
{
    public static class CommonList
    {
        public static List<Category> GetAllCategory()
        {
            var lst = new CategoryDAO().GetAllCategory();
            if (lst.Any())
                return lst;
            else return new List<Category>();
        }

        public static List<ProductCategory> GetAllProductCategory()
        {
            var lst = new ProductCategoryDAO().GetListAll();
            if (lst.Any())
                return lst;
            return new List<ProductCategory>();
        }
        public static bool CheckProductInCategory(long categoryId)
        {
            var lst = new ProductDAO().ListProductByCategoryId(4,categoryId);
            return lst.Count() > 0;
        }
    }
}