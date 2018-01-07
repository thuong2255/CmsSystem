using System.Web.Mvc;
using System.Web.Routing;

namespace CmsSystem.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Menu",
                url: "quan-ly-menu",
                defaults: new { controller = "Action", action = "Index" },
                namespaces: new string[] { "CmsSystem.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Role",
                url: "quan-ly-quyen",
                defaults: new { controller = "Role", action = "Index" },
                namespaces: new string[] { "CmsSystem.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "Home", action = "Login" },
                namespaces: new string[] { "CmsSystem.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Function",
                url: "quan-ly-chuc-nang",
                defaults: new { controller = "Function", action = "Index" },
                namespaces: new string[] { "CmsSystem.Web.Controllers" }
            );
            routes.MapRoute(
                name: "User",
                url: "quan-ly-nguoi-dung",
                defaults: new { controller = "User", action = "Index" },
                namespaces: new string[] { "CmsSystem.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}