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
                "GetData",
                "getdata/{id}/{polinom}",
             new { controller = "Home", action = "GetData", id = UrlParameter.Optional, polinom = UrlParameter.Optional }
            );

            routes.MapLocalizedRoute(
                name: "specialities",
                url: "specialities",
                defaults: new { controller = "BackOffice", action = "Specialities" },
                setupConstraints:
                (dynamic constraints) =>
                {
                });

            routes.MapLocalizedRoute(
                name: "saveSpeciality",
                url: "BackOffice/Save",
                defaults: new { controller = "BackOffice", action = "Save" },
                setupConstraints:
                (dynamic constraints) =>
                {
                });


            routes.MapLocalizedRoute(
              name: "editSpeciality",
              url: "specialities/edit",
              defaults: new { controller = "BackOffice", action = "EditSpecialities" },
              setupConstraints:
              (dynamic constraints) =>
              {
              });

            routes.MapLocalizedRoute(
             name: "addSpeciality",
             url: "specialities/add",
             defaults: new { controller = "BackOffice", action = "AddSpeciality", viewModel = UrlParameter.Optional },
             setupConstraints:
             (dynamic constraints) =>
             {
             });


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

            routes.MapLocalizedRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "User", action = "LogIn"},
                setupConstraints:
                (dynamic constraints) =>
                {
                });

            routes.MapLocalizedRoute(
                name: "LogOut",
                url: "logout",
                defaults: new { controller = "User", action = "LogOut"},
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