using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PostMidProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapMvcAttributeRoutes();

            //Custom Routes
            //routes.MapRoute(
            //    "AdminInterface",
            //    "{Admin}/{action}/{id}",
            //    new {controller="Admin",action="AdminDashboard", id= UrlParameter.Optional }
            //    );

            //routes.MapRoute(
            //    "CustomerInterface",
            //    "{Customer}/{action}/{id}",
            //    new { controller = "Customer", action = "buyGlasses", id = UrlParameter.Optional }
            //    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
