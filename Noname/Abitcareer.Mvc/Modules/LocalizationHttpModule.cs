using CultureEngine;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Routing;
using Abitcareer.Mvc.Extensions;

namespace Abitcareer.Mvc
{
    public class LocalizationHttpModule : IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(OnBeginRequest);
        }

        private void OnBeginRequest(Object sender, EventArgs args)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);

            var userCulture = CEngine.Instance.GetCultureByUserLanguages(application.Request.UserLanguages);

            if (currentContext.Request.Url.AbsolutePath.Equals("") ||
                currentContext.Request.Url.AbsolutePath.Equals("/"))
            {
                currentContext.Response.RedirectToRoute("Default", new { locale = userCulture });
                return;
            }

            RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);
            var cultureName = routeData.Values[CEngine.Instance.CultureKey];
            if (cultureName != null)
            {
                if (CEngine.Instance.IsSupported(cultureName.ToString()))
                {
                    CEngine.Instance.SetCultureForThread(cultureName.ToString());
                }
                else
                {
                    currentContext.Response.RedirectToRoute("Default", new { locale = userCulture });
                }
            }
            else
            {
                CEngine.Instance.SetCultureForThread(userCulture);
            }
        }
    }
}
