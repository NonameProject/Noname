using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Abitcareer.Mvc.App_Start
{
    public class MiniprofilerConfig
    {
        //for specific users
        private bool IsUserAllowedToSeeMiniProfilerUI(HttpRequest httpRequest)
        {
            var principal = httpRequest.RequestContext.HttpContext.User;
            return principal.IsInRole("admin");
        }

        private static bool DisableProfilingResults { get; set; }

        public static void InitProfilerSettings()
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
    }
}
