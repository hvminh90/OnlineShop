using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class ProductModelEdit
    {
        public long ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticResource.Resources), ErrorMessageResourceName = "rqProductName")]
        [Display(Name = "ProductName", ResourceType = typeof(StaticResource.Resources))]
        public string Name { get; set; }


        [Required(ErrorMessageResourceType = typeof(StaticResource.Resources), ErrorMessageResourceName = "rqProductCode")]
        [Display(Name = "ProductCode", ResourceType = typeof(StaticResource.Resources))]
        public string Code { get; set; }


        [Required(ErrorMessageResourceType = typeof(StaticResource.Resources), ErrorMessageResourceName = "rqProductDescription")]
        [Display(Name = "ProductDescription", ResourceType = typeof(StaticResource.Resources))]
        public string Description { get; set; }


        [Display(Name = "ProductImage", ResourceType = typeof(StaticResource.Resources))]
        public string Image { get; set; }

        [Display(Name = "ProductMoreImage", ResourceType = typeof(StaticResource.Resources))]
        public string MoreImages { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticResource.Resources), ErrorMessageResourceName = "rqProductPrice")]
        [Display(Name = "ProductPrice", ResourceType = typeof(StaticResource.Resources))]
        public string Price { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticResource.Resources), ErrorMessageResourceName = "rqProductQuantity")]
        [Display(Name = "ProductQuantity", ResourceType = typeof(StaticResource.Resources))]
        public int? Quantity { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticResource.Resources), ErrorMessageResourceName = "rqProductCategory")]
        [Display(Name = "ProductCategory", ResourceType = typeof(StaticResource.Resources))]
        public long? CategoryID { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticResource.Resources), ErrorMessageResourceName = "rqProductDetail")]
        [Display(Name = "ProductDetail", ResourceType = typeof(StaticResource.Resources))]
        public string Detail { get; set; }

        [Display(Name = "ProductStatus", ResourceType = typeof(StaticResource.Resources))]
        public bool Status { get; set; }

        [Display(Name = "ProductTopHot", ResourceType = typeof(StaticResource.Resources))]
        public DateTime? TopHot { get; set; }

        [NotMapped]
        public List<ImageProduct> ListImageProduct { get; set; }

        [NotMapped]
        public string StringImageProduct { get; set; }

    }
}