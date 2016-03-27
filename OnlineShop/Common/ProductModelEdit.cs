using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Common
{
    public class ProductModelEdit
    {
        public Product Product { get; set; }
        public List<ImageProduct> ListImageProduct { get; set; }

        public string StringImageProduct { get; set; }
    }
}