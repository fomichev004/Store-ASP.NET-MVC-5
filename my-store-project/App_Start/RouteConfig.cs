﻿using System;
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
            // 23
            routes.MapRoute("Account", "Account/{action}/{id}", new {controller = "Account", action = "Index", id = UrlParameter.Optional }, 
                new []{"my_store_project.Controllers"}); 
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // 20
             routes.MapRoute("Cart", "Cart/{action}/{id}", new {controller = "Cart", action = "Index", id = UrlParameter.Optional }, 
                new []{"my_store_project.Controllers"});            
            
            // 18
            routes.MapRoute("SidebarPartial", "Pages/SidebarPartial", new {controller = "Pages", action = "SidebarPartial"}, 
                new []{"my_store_project.Controllers"});
                
             routes.MapRoute("Shop", "Shop/{action}/{name}", new {controller = "Shop", action = "Index", name = UrlParameter.Optional }, 
                new []{"my_store_project.Controllers"});

            routes.MapRoute("PagesMenuPartial", "Pages/PagesMenuPartial", new {controller = "Pages", action = "PagesMenuPartial"}, 
                new []{"my_store_project.Controllers"});
            
            routes.MapRoute("Pages", "{page}", new {controller = "Pages", action = "Index"}, 
                new []{"my_store_project.Controllers"});

            routes.MapRoute("Default", "", new {controller = "Pages", action = "Index"}, 
                new []{"my_store_project.Controllers"});

            // routes.MapRoute(
            //     name: "Default",
            //     url: "{controller}/{action}/{id}",
            //     defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            // );
        }
    }
}
