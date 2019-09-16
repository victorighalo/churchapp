﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KingdomJoy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Api",
                url: "api/{controller}/{id}",
                defaults: new {id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ApiWithAction",
                url: "api/{controller}/{action}/{id}"
            );
        }
    }
}
