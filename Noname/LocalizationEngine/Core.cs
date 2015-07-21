using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace LocalizationEngine
{
    /// <summary>
    ///     Static class which contains methods that provides culture actions: changing current culture, getting localized string etc.
    /// </summary>
    public static class LEngine
    {
        /// <summary>
        ///     List of supported cultures
        /// </summary>
        private static List<string> SupportedCultures = new List<string>();

        /// <summary>
        /// Key for accessing stored cookies
        /// </summary>
        public const string CookieCultureKey = "lengine-locale";

        /// <summary>
        ///     Default culture name
        /// </summary>
        private const string DefaultCulture = "uk-UA";

        /// <summary>
        ///     Method that creates resource manager for new culture and adds that culture to SupportedCultures list
        /// </summary>
        /// <param name="cultureName">Name of culture that will be registered</param>
        private static void RegisterCulture(string cultureName)
        {
            if (!SupportedCultures.Contains(cultureName))
            {
                SupportedCultures.Add(cultureName);
            }
        }

        /// <summary>
        /// Methor that set culture for thread based on culture name
        /// </summary>
        /// <param name="cultureName">Name of current culture</param>
        public static void SetCultureForThread(HttpRequest request)
        {
            string cultureName = DefaultCulture;
            if (request.Cookies[CookieCultureKey] != null 
                && SupportedCultures.Contains(request.Cookies[CookieCultureKey].Value))
            {
                cultureName = request.Cookies[CookieCultureKey].Value;
            }
            var cultureInfo = CultureInfo.GetCultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        /// <summary>
        ///     Static constructor that contains registrations of supported cultures
        /// </summary>
        static LEngine()
        {
            RegisterCulture(DefaultCulture);
            RegisterCulture("uk-UA");
            RegisterCulture("en-US");
        }

        /// <summary>
        ///     Method that set changes current culture
        /// </summary>
        /// <param name="newCultureName">Name of new culture. If culture with this name is not supported, it will be replaced with default value</param>
        /// <param name="context">Request context that contains cookies-file with culture data</param>
        public static void SetCulture(string newCultureName, RequestContext context)
        {
            if (SupportedCultures.Contains(newCultureName))
            {
                context.HttpContext.Response.AppendCookie(new HttpCookie(CookieCultureKey, newCultureName));
            }
            else
            {
                context.HttpContext.Response.AppendCookie(new HttpCookie(CookieCultureKey, DefaultCulture));
            }
        }
    }
}
