using Abitcareer.Business.Components.MiniProfilers;
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
            MiniProfiler.Stop();
        }

        //for specific users
        private bool IsUserAllowedToSeeMiniProfilerUI(HttpRequest httpRequest)
        {
            var principal = httpRequest.RequestContext.HttpContext.User;
            return principal.IsInRole("admin");
        }
        public static bool DisableProfilingResults { get; set; }
        private void InitProfilerSettings()
        {
            // by default, sql parameters won't be displayed
            MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
            MiniProfiler.Settings.ShowControls = false;
            MiniProfiler.Settings.StackMaxLength = 256;
            MiniProfiler.Settings.Results_Authorize = request =>
            {
                if ("/Home/ResultsAuthorization".Equals(request.Url.LocalPath, StringComparison.OrdinalIgnoreCase))
                {
                    return (request.Url.Query).ToLower().Contains("isauthorized");
                }
                return !DisableProfilingResults;
            };
            MiniProfiler.Settings.Results_List_Authorize = request =>
            {
                return true;
            };
        }

        public override void Init()
        {
            base.Init();
            Module.Init(this);
        }
        //Database
        /*public static DbConnection GetOpenConnection()
        {
            var cnn = CreateRealConnection(); // A SqlConnection, SqliteConnection ... or whatever

            // wrap the connection with a profiling connection that tracks timings 
            return new StackExchange.Profiling.Data.ProfiledDbConnection(cnn, MiniProfiler.Current);
        }*/
    }
}