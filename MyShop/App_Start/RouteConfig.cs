using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet/{metatitle}-{productId}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );
            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{metatitle}-{cateId}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );

            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );

            routes.MapRoute(
                name: "Delete",
                url: "xoa-san-pham-{idProduct}",
                defaults: new { controller = "Cart", action = "Delete", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );

            routes.MapRoute(
                name: "Pay",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Pay", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );

            routes.MapRoute(
                name: "Success",
                url: "hoan-thanh",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ki",
                defaults: new { controller = "Login", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "dang-xuat",
                defaults: new { controller = "Login", action = "Logout", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyShop.Controllers" }
            );
        }
    }
}
