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
    ///     Static class which contains methods that provides localization actions: changing current localization, getting localized string etc.
    /// </summary>
    public static class LEngine
    {
        /// <summary>
        ///     List of supported localization's
        /// </summary>
        private static List<string> SupportedLocalizations = new List<string>();

        /// <summary>
        /// Key for accessing stored cookies
        /// </summary>
        public const string CookieCultureKey = "lengine-locale";

        /// <summary>
        ///     Default localization name
        /// </summary>
        private const string DefaultCulture = "ru-RU";

        /// <summary>
        ///     Method that creates resource manager for new culture and adds that culture to SupportedLocalizations list
        /// </summary>
        /// <param name="cultureName">Name of culture that will be registered</param>
        private static void RegisterLocalization(string cultureName)
        {
            if (!SupportedLocalizations.Contains(cultureName))
            {
                SupportedLocalizations.Add(cultureName);
            }
        }

        /// <summary>
        /// Methor that set culture for thread based on culture name
        /// </summary>
        /// <param name="cultureName">Name of current culture</param>
        public static void SetCultureForThread(HttpRequest request)
        {
            string cultureName = DefaultCulture;
            if (request.Cookies[CookieCultureKey] != null)
            {
                cultureName = request.Cookies[CookieCultureKey].Value;
            }
            var cultureInfo = CultureInfo.GetCultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        /// <summary>
        ///     Static constructor that contains registrations of supported localizations
        /// </summary>
        static LEngine()
        {
            RegisterLocalization(DefaultCulture);
            RegisterLocalization("uk-UA");
            RegisterLocalization("en-US");
        }

        /// <summary>
        ///     Method that set changes current localization
        /// </summary>
        /// <param name="newLocalization">Name of new localization. If localization with this name is not supported, it will be replaced with default value</param>
        /// <param name="context">Request context that contains cookies-file with localization data</param>
        public static void SetLocalization(string newLocalization, RequestContext context)
        {
            if (SupportedLocalizations.Contains(newLocalization))
            {
                context.HttpContext.Response.AppendCookie(new HttpCookie(CookieCultureKey, newLocalization));
            }
            else
            {
                context.HttpContext.Response.AppendCookie(new HttpCookie(CookieCultureKey, DefaultCulture));
            }
        }
    }
}
