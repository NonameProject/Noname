using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Localization
{
    public class LocalizedRouteHandler : MvcRouteHandler
    {
        protected override System.Web.IHttpHandler GetHttpHandler(System.Web.Routing.RequestContext requestContext)
        {
            var urlLocale = requestContext.RouteData.Values["culture"] as string;
            var cultureNameFromUrl = urlLocale ?? "";
            var currentCultureName = Thread.CurrentThread.CurrentCulture.Name;
            if (!SupportedCultures.Exist(cultureNameFromUrl))
            {
                return base.GetHttpHandler(requestContext);
            }
            if (!currentCultureName.Equals(cultureNameFromUrl))
            {
                var culture = CultureInfo.GetCultureInfo(cultureNameFromUrl);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            return base.GetHttpHandler(requestContext);
        }
    }
}