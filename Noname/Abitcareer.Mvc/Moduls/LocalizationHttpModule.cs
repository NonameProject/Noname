﻿using CultureEngine;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Routing;

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
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);

            if (currentContext.Request.Url.AbsolutePath.Equals("") ||
                currentContext.Request.Url.AbsolutePath.Equals("/"))
            {
                SetCulture(Thread.CurrentThread.CurrentCulture.Name);
                currentContext.Response.RedirectToRoute("Default", new { locale = Thread.CurrentThread.CurrentCulture.Name });
                return;
            }
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
                    currentContext.Response.RedirectToRoute("Default", new { locale = CEngine.DefaultCulture });
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
