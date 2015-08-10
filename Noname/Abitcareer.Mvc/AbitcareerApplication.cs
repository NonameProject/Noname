using Abitcareer.Business.Components.MiniProfilers;
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
            DefaultModelBinder.ResourceClassKey = "LocalizationResx";

        }

        public void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            var httpException = e.Exception as JavaScriptException;
            if (httpException != null)
            {
                e.Dismiss();
            }
        }

        protected void Application_BeginRequest()
        {
            //MiniProfiler.Start();//if you need to connect the included miniProfiler
            GlobalFilters.Filters.Add(new ProfilingActionFilter());
            RewriteViewEngines();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        private void RewriteViewEngines()
        {
            var copy = ViewEngines.Engines.ToList();
            ViewEngines.Engines.Clear();
            foreach (var item in copy)
            {
                ViewEngines.Engines.Add(new ProfilingViewEngine(item));
            }
        }

        private void Application_Error(Object sender, EventArgs args)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);            
            var userCulture = CEngine.Instance.GetCultureByUserLanguages(application.Request.UserLanguages);

            RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);
            var cultureName = routeData.Values[CEngine.Instance.CultureKey];
            var hasCultureSegment = Regex.IsMatch(currentContext.Request.Path, "[A-z]{2}-[A-z]{2}");
            if (!hasCultureSegment && cultureName == null)
            {
                CEngine.Instance.SetCultureForThread(userCulture);
                Response.Clear();
                currentContext.Response.Redirect(userCulture + currentContext.Request.Path);
                currentContext.ClearError();
            }            
        }
    }
}