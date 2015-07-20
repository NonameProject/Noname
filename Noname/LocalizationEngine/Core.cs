using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace LocalizationEngine
{
    public class LEngine
    {
        private static List<string> SupportedLocalizations = new List<string>();

        private static Dictionary<string, ResourceManager> ResourceManagers = new Dictionary<string, ResourceManager>();

        private static string DefaultLocalization;

        private static void RegisterLocalization(string cultureName)
        {
            SupportedLocalizations.Add(cultureName);
            var resourceName = (cultureName == DefaultLocalization) ? "LocalizationEngine.Localization" : "LocalizationEngine.Localization." + cultureName.Replace('-', '.');
            ResourceManagers[cultureName] = new ResourceManager(resourceName, typeof(LEngine).Assembly);
        }
        static LEngine()
        {
            DefaultLocalization = "ru-RU";

            RegisterLocalization("ru-RU");
            RegisterLocalization("uk-UA");
            RegisterLocalization("en-US");
        }
        public static string GetLocalizedString(string key, RequestContext context)
        {
            var currentLocalization = context.HttpContext.Request.Cookies["locale"].Value;
            return ResourceManagers[currentLocalization].GetString(key);
        }

        public static void SetLocalization(string newLocalization, RequestContext context)
        {
            if (SupportedLocalizations.Contains(newLocalization))
                context.HttpContext.Response.AppendCookie(new HttpCookie("locale", newLocalization));
            else
                context.HttpContext.Response.AppendCookie(new HttpCookie("locale", DefaultLocalization));
        }
    }
}
