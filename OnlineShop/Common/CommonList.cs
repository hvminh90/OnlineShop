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
    }
}