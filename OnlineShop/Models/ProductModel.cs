using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class ProductModel
    {
        public Product product { get; set; }
        public string CategoryName { get; set; }
    }
}