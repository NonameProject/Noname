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
        ///     Dictionary that contains resource managers linked to localized resource file.
        /// </summary>
        private static Dictionary<string, ResourceManager> ResourceManagers = new Dictionary<string, ResourceManager>();

        /// <summary>
        ///     Default localization name
        /// </summary>
        private const string DefaultLocalization = "ru-RU";

        /// <summary>
        ///     Method that creates resource manager for new culture and adds that culture to SupportedLocalizations list
        /// </summary>
        /// <param name="cultureName">Name of culture that will be registered</param>
        private static void RegisterLocalization(string cultureName)
        {
            SupportedLocalizations.Add(cultureName);
            var resourceName = (cultureName == DefaultLocalization) ? "LocalizationEngine.Localization" : "LocalizationEngine.Localization." + cultureName.Replace('-', '.');
            ResourceManagers[cultureName] = new ResourceManager(resourceName, typeof(LEngine).Assembly);
        }

        /// <summary>
        ///     Static constructor that contains registrations of supported localizations
        /// </summary>
        static LEngine()
        {
            RegisterLocalization(DefaultLocalization);
            RegisterLocalization("uk-UA");
            RegisterLocalization("en-US");
        }

        /// <summary>
        ///     Method that returns localized string by key from resource based on current localization 
        /// </summary>
        /// <param name="key">Alias for string</param>
        /// <param name="context">Request context that contains cookies-file with localization data</param>
        /// <returns>Returns localized string</returns>
        public static string GetLocalizedString(string key, RequestContext context)
        {
            var currentLocalization = context.HttpContext.Request.Cookies["locale"].Value;
            return ResourceManagers[currentLocalization].GetString(key);
        }

        /// <summary>
        ///     Method that set changes current localization
        /// </summary>
        /// <param name="newLocalization">Name of new localization. If localization with this name is not supported, it will be replaced with default value</param>
        /// <param name="context">Request context that contains cookies-file with localization data</param>
        public static void SetLocalization(string newLocalization, RequestContext context)
        {
            if (SupportedLocalizations.Contains(newLocalization))
                context.HttpContext.Response.AppendCookie(new HttpCookie("locale", newLocalization));
            else
                context.HttpContext.Response.AppendCookie(new HttpCookie("locale", DefaultLocalization));
        }
    }
}
