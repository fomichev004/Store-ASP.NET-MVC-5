using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace my_store_project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("PagesMenuPartial", "{Pages/PagesMenuPartial}", new {controller = "Pages", action = "Index"}, new []{"PagesMenuPartial"});
            
            routes.MapRoute("Pages", "{page}", new {controller = "Pages", action = "Index"}, new []{"my_store_project.Controllers"});
            routes.MapRoute("Default", "", new {controller = "Pages", action = "Index"}, new []{"my_store_project.Controllers"});
            // routes.MapRoute(
            //     name: "Default",
            //     url: "{controller}/{action}/{id}",
            //     defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            // );
        }
    }
}
