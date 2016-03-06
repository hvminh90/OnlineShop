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
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }

            );
            routes.MapRoute(
                name: "dangnhap",
                url: "dang-nhap",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }

            );
            routes.MapRoute(
                name: "dangky",
                url: "dang-ky",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
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
