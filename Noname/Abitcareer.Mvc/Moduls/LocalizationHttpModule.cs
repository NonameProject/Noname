using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using CultureEngine;
namespace Abitcareer.Mvc
{
    public class LocalizationHttpModule : IHttpModule
    {

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(OnBeginRequest);
        }

        private void OnBeginRequest(Object sender, EventArgs args)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            if (context.Request.Url.AbsolutePath.Equals("") ||
                context.Request.Url.AbsolutePath.Equals("/"))
            {
                SetCulture(Thread.CurrentThread.CurrentCulture.Name);
                context.Response.RedirectToRoute("Default", new { locale = Thread.CurrentThread.CurrentCulture.Name });
                return;
            }
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);
            var cultureName = routeData.Values[CEngine.CultureKey];
            if (cultureName != null)
            {
                if (CEngine.IsSupported(cultureName.ToString()))
                {
                    SetCulture(cultureName.ToString());
                }
                else
                {
                    SetCulture(CEngine.DefaultCulture);
                    context.Response.RedirectToRoute("Default", new { locale = CEngine.DefaultCulture });
                }
            }
        }

        private void SetCulture(string cultureName)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
    }
}
