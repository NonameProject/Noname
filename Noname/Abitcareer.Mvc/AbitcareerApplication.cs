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

namespace Abitcareer.Mvc
{
    public class AbitcareerApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ControllerBuilder.Current.DefaultNamespaces.Add("Abitcareer.Mvc.Controllers");
            AutomapperConfig.RegisterMaps();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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
            MiniProfiler.Start();
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
    }
}