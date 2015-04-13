using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExtendedBlog.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "post/page{pageNumber}",
                defaults: new { controller = "Post", action = "List", pageNumber = 1 }
            );

            routes.MapRoute(
                null,
                "post/{postUrl}",
                new { controller = "Post", action = "FullPost" }
            );

            routes.MapRoute(
                null,
                "post/{tagUrl}/page{pageNumber}",
                new { controller = "Post", action = "PostsForTag", pageNumber = 1 }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Post", action = "List", id = UrlParameter.Optional }
                );
        }
    }
}