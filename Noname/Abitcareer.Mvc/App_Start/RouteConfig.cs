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

            routes.Add(new Route("handl/{pathInfo}", new PersonImageRouteHandler()));
            routes.IgnoreRoute("handl/{*url}");

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

            routes.MapLocalizedRoute(
              name: "IsSpecialityNameAvailable",
              url: "IsSpecialityNameAvailable",
              defaults: new { controller = "BackOffice", action = "IsSpecialityNameAvailable" },
              setupConstraints:
              (dynamic constraints) =>
              {
              });

            routes.MapLocalizedRoute(
             name: "IsSpecialityEnglishNameAvailable",
             url: "IsSpecialityEnglishNameAvailable",
             defaults: new { controller = "BackOffice", action = "IsSpecialityEnglishNameAvailable" },
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
                url: "BackOffice/EditSpeciality",
                defaults: new { controller = "BackOffice", action = "EditSpeciality" },
                setupConstraints:
                (dynamic constraints) =>
                {
                });


            routes.MapLocalizedRoute(
              name: "editSpeciality",
              url: "specialities/edit",
              defaults: new { controller = "BackOffice", action = "EditSpeciality" },
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

            routes.MapLocalizedRoute(
               name: "DeleteSpeciality",
               url: "deletespeciality",
               defaults: new { controller = "BackOffice", action = "DeleteSpeciality" },
               setupConstraints:
               (dynamic constraints) =>
               {
               });

            routes.MapLocalizedRoute(
             name: "IndexSpecialities",
             url: "indexspecialities",
             defaults: new { controller = "BackOffice", action = "IndexSpecialities" },
             setupConstraints:
             (dynamic constraints) =>
             {
             });

             routes.MapLocalizedRoute(
             name: "SearchForSpeaciality",
             url: "searchforspeaciality",
             defaults: new { controller = "BackOffice", action = "SearchForSpeaciality" },
             setupConstraints:
             (dynamic constraints) =>
             {
             });

             routes.MapLocalizedRoute(
             name: "Import",
             url: "import",
             defaults: new { controller = "DataImport", action = "Import" },
             setupConstraints:
             (dynamic constraints) =>
             {
             });


             routes.MapLocalizedRoute(
              name: "LocalizedNotFound",
              url: "{*url}",
              defaults: new { controller = "Error", action = "Index" },
              setupConstraints:
              (dynamic constraints) => {
              });

             routes.MapRoute(
                 "NotFound",
                 "{*url}",
              new { controller = "Error", action = "Abc" }
             );
        }
    }
}