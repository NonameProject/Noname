using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentList
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "createStudent",
               url: "Student/create",
               defaults: new { controller = "Student", action = "Create" }
           );

            routes.MapRoute(
            name: "studentList",
            url: "Student/list",
            defaults: new { controller = "Student", action = "List" }
        );

            routes.MapRoute(
             name: "LogOff",
             url: "Account/LogOff",
             defaults: new { controller = "Account", action = "LogOff" }
         );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Student", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}