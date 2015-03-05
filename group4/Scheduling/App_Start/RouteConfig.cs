using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Scheduling
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Search",
            //    url: "Search/{saved}",
            //    defaults: new { controller = "Search", action = "Search", saved = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Index",
                url: "Share",
                defaults: new { controller = "Share", action = "Index" }
                );
            routes.MapRoute(
                name: "UpdateCategoryHandler",
                url: "Share/UpdateCategoryHandler",
                defaults: new { controller = "Share", action = "UpdateCategoryHandler" }
                );
            routes.MapRoute(
                name:"Share",
                url: "Share/{id}",
                defaults: new { controller = "Share", action = "Fetch",id=UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "PartialAgendaCategory",
                url: "Calendar/PartialAgendaCategory/{id}",
                defaults: new { controller = "Calendar", action = "PartialAgendaCategory", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "PartialMonthCategory",
                url: "Calendar/PartialMonthCategory/{id}",
                defaults: new { controller = "Calendar", action = "PartialMonthCategory", id = UrlParameter.Optional }
            );  
            routes.MapRoute(
                name: "Calendar",
                url: "Calendar/{action}/{applicationCode}",
                defaults: new { controller = "Calendar", action = "Index", applicationCode = UrlParameter.Optional }
            );          

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}