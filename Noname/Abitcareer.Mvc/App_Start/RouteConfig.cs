using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Abitcareer.Mvc.Extensions;

namespace Abitcareer.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("elmah.axd");

            routes.MapLocalizedRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index" },
                setupConstraints:
                (dynamic constraints) =>
                {
                });

            routes.MapLocalizedRoute(
                name: "Home",
                url: "Home",
                defaults: new { controller = "Home", action = "Index" },
                setupConstraints:
                (dynamic constraints) =>
                {
                });

            routes.MapRoute(
               name: "specialities",
               url: "specialities",
               defaults: new { controller = "BackOffice", action = "Specialities" });


            routes.MapRoute(
               name: "saveSpeciality",
               url: "BackOffice/Save",
               defaults: new { controller = "BackOffice", action = "Save" });

            routes.MapRoute(
                name: "editSpecialities",
                url: "editSpecialities/{id}",
                defaults: new { controller = "BackOffice", action = "EditSpecialities", id = UrlParameter.Optional });
             

            routes.MapLocalizedRoute(
                name: "ChangeCulture",
                url: "ChangeCulture/{culture}",
                defaults: new { controller = "LocalizationEngine", action = "ChangeCulture" },
                setupConstraints:
                (dynamic constraints) =>
                {
                });

            routes.MapLocalizedRoute(
                name: "TestDb",
                url: "TestDb",
                defaults: new { controller = "LocalizationEngine", action = "TestDb" },
                setupConstraints:
                (dynamic constraints) =>
                {
                });

            routes.MapRoute(
                "NotFound",
                "{*url}",
             new { controller = "Error", action = "Index" }
            );
        }
    }
}