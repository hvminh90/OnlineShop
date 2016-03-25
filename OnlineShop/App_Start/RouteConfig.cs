using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "SanPham",
                url: "san-pham/{metatitle}-{categoryID}/{page}",
                defaults: new
                {
                    controller = "Product",
                    action = "Category",
                    page = UrlParameter.Optional
                },
                namespaces: new[] { "OnlineShop.Controllers" }

            );
            routes.MapRoute(
               name: "AllSanPham",
               url: "all-san-pham",
               defaults: new
               {
                   controller = "Product",
                   action = "AllProduct" 
                   
               },
               namespaces: new[] { "OnlineShop.Controllers" }

           );
            routes.MapRoute(
                name: "AddCart",
                url: "them-gio-hang-{productId}-{quantity}",
                defaults: new
                {
                    controller = "Cart",
                    action = "AddItem",
                    quantity = UrlParameter.Optional
                },
                namespaces: new[] { "OnlineShop.Controllers" }

            );

            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new
                {
                    controller = "Cart",
                    action = "Index"

                },
                namespaces: new[] { "OnlineShop.Controllers" }

            );

            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new
                {
                    controller = "Cart",
                    action = "Payment"

                },
                namespaces: new[] { "OnlineShop.Controllers" }

            );

            routes.MapRoute(
                name: "lienhe",
                url: "lien-he",
                defaults: new 
                { 
                    controller = "Contact", 
                    action = "Index", 
                    id = UrlParameter.Optional 
                },
                namespaces: new[] { "OnlineShop.Controllers" }

            );
            routes.MapRoute(
                name: "dangnhap",
                url: "dang-nhap",
                defaults: new 
                { 
                    controller = "Account", 
                    action = "Login", 
                    id = UrlParameter.Optional 
                },
                namespaces: new[] { "OnlineShop.Controllers" }

            );
            routes.MapRoute(
                name: "dangky",
                url: "dang-ky",
                defaults: new 
                { 
                    controller = "Account",
                    action = "Register", 
                    id = UrlParameter.Optional 
                },
                namespaces: new[] { "OnlineShop.Controllers" }

            );
            routes.MapRoute(
               name: "timkiem",
               url: "tim-kiem",
               defaults: new 
               { 
                   controller = "Product",
                   action = "Search", 
                   id = UrlParameter.Optional
               },
               namespaces: new[] { "OnlineShop.Controllers" }

           );

            routes.MapRoute(
               name: "tintuc",
               url: "tin-tuc/{page}",
               defaults: new 
               { 
                   controller = "Content",
                   action = "Index", 
                   page = UrlParameter.Optional 
               },
               namespaces: new[] { "OnlineShop.Controllers" }

           );
            routes.MapRoute(
               name: "tintucchitiet",
               url: "tin-tuc-detail/{metatitle}-{id}",
               defaults: new
               { 
                   controller = "Content",
                   action = "Detail", 
                   id = UrlParameter.Optional 
               },
               namespaces: new[] { "OnlineShop.Controllers" }

           );
            routes.MapRoute(
              name: "Tags",
              url: "tag/{tagId}/{page}",
              defaults: new 
              { 
                  controller = "Content",
                  action = "Tags",
                  page = UrlParameter.Optional 
              },
              namespaces: new[] { "OnlineShop.Controllers" }

          );
            routes.MapRoute(
              name: "Error403",
              url: "error/403",
              defaults: new 
              { 
                  controller = "Home",
                  action = "Error403", 
                  id = UrlParameter.Optional 
              },
              namespaces: new[] { "OnlineShop.Controllers" }

          );

            routes.MapRoute(
              name: "gioithieu",
              url: "gioi-thieu",
              defaults: new 
              { 
                  controller = "Home",
                  action = "About", 
                  id = UrlParameter.Optional 
              },
              namespaces: new[] { "OnlineShop.Controllers" }

          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }

            );


        }
    }
}
