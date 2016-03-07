﻿using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CategoryDAO
    {
         OnlineShopDbContext db = null;
         public CategoryDAO()
       {
           db = new OnlineShopDbContext();

       }

        public List<Category> GetAllCategory()
        {
            return db.Categories.Where(x=>x.Status== true).ToList();
        }
    }
}
