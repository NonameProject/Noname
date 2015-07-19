using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Collections.Generic;

namespace LocalizationEngine
{
    public class LEngine
    {
        private static List<string> SupportedLocalizations = new List<string>();

        private static string DefaultLocalization;
        static LEngine()
        {
            SupportedLocalizations.Add("ru-RU");
            SupportedLocalizations.Add("uk-UA");
            SupportedLocalizations.Add("en-US");

            DefaultLocalization = "ru-RU";
        }
        public static string GetLocalizedString(string key, CultureInfo culture)
        {
            if (!SupportedLocalizations.Contains(culture.Name))
                culture = new CultureInfo(DefaultLocalization);
            var resourceName = (culture.Name == DefaultLocalization) ? "LocalizationEngine.Localization" : "LocalizationEngine.Localization." + culture.Name.Replace('-', '.');

            ResourceManager rm = new ResourceManager(resourceName, typeof(LEngine).Assembly);
            return rm.GetString(key);
        }
    }
}
