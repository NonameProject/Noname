using Abitcareer.Business.Components.MiniProfilers;
using Abitcareer.Mvc.App_Start;
using Abitcareer.Mvc.Components.CustomExceptions;
using Elmah;
using StackExchange.Profiling;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Abitcareer.Mvc
{
    public class AbitcareerApplication : System.Web.HttpApplication
    {
        private IHttpModule Module = new LocalizationHttpModule();

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
            MiniprofilerConfig.StartMiniprofiler();
            MiniProfiler.Start();
            // Add Profiling Action Filter (mvc mini profiler)
            GlobalFilters.Filters.Add(new ProfilingActionFilter());
            // Add Profiling View Engine (mvc mini profiler)
            var copy = ViewEngines.Engines.ToList();
            ViewEngines.Engines.Clear();
            foreach (var item in copy)
            {
                ViewEngines.Engines.Add(new ProfilingViewEngine(item));
            }

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_EndRequest()
        {
            MiniprofilerConfig.StopMiniprofiler();
        }        

        public override void Init()
        {
            base.Init();
            Module.Init(this);
        }
    }
}