using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class ProductModelView
    {
        public Product product { get; set; }
        public string CategoryName { get; set; }
    }
}