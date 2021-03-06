﻿using Abitcareer.Mvc.MiniProfilers;
using Abitcareer.Mvc.App_Start;
using Elmah;
using StackExchange.Profiling;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abitcareer.Core.CustomExceptions;
using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using Abitcareer.Mvc.Components;
using CultureEngine;
using System.Text.RegularExpressions;
using Abitcareer.Business.Components.Managers;

namespace Abitcareer.Mvc
{
    public class AbitcareerApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ControllerBuilder.Current.DefaultNamespaces.Add("Abitcareer.Mvc.Controllers");
            AutofacConfig.RegisterDependencies();
            AutomapperConfig.RegisterMaps();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(SpecialityViewModel), new SpecialityBinder());
            ModelBinders.Binders.Add(typeof(SpecialityAdvancedViewModel), new SpecialityAdvancedModelBinder());
            DefaultModelBinder.ResourceClassKey = "LocalizationResx";
            GlobalFilters.Filters.Add(new ProfilingActionFilter());
            var copy = ViewEngines.Engines.ToList();
            ViewEngines.Engines.Clear();
            foreach (var item in copy)
            {
                ViewEngines.Engines.Add(new ProfilingViewEngine(item));
            }
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        public void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            var httpException = e.Exception as JavaScriptException;
            if (httpException != null)
            {
                e.Dismiss();
            }
        }

        protected void Application_AuthenticateRequest()
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}