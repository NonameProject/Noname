using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;

namespace CultureEngine
{
    /// <summary>
    ///     Static class which contains methods that provides culture actions: changing current culture, getting localized string etc.
    /// </summary>
    public static class CEngine
    {
        /// <summary>
        ///     List of supported cultures
        /// </summary>
        private static List<string> SupportedCultures = new List<string>();

        /// <summary>
        /// Key for accessing stored cookies
        /// </summary>
        public const string CultureKey = "locale";

        /// <summary>
        ///     Default culture name
        /// </summary>
        public const string DefaultCulture = "uk-UA";

        /// <summary>
        ///     Method that creates resource manager for new culture and adds that culture to SupportedCultures list
        /// </summary>
        /// <param name="cultureName">Name of culture that will be registered</param>
        private static void RegisterCulture(string cultureName)
        {
            if(!SupportedCultures.Any(s => s.Equals(cultureName, StringComparison.OrdinalIgnoreCase)))
            {
                SupportedCultures.Add(cultureName);
            }
        }

        /// <summary>
        /// Check is supported culture or not
        /// </summary>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        public static bool IsSupported(string cultureName)
        {
            return SupportedCultures.Any(s => s.Equals(cultureName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Parse user languages array from request and return user's browser culture or default application culture name
        /// </summary>
        /// <param name="userLanguages"></param>
        /// <returns></returns>
        public static string GetCultureByUserLanguages(string[] userRequestLanguages)
        {
            int userLanguagesLength = userRequestLanguages.Length;
            Char[] splitArray = new Char[] { ';' };
            for (int i = 0; i < userLanguagesLength; i++)
            {
                var culture = userRequestLanguages[i].Split(splitArray)[0];
                foreach (var item in SupportedCultures)
                {
                    if (item.Equals(culture, StringComparison.OrdinalIgnoreCase))
                    {
                        return item;
                    }
                }
            }
            return DefaultCulture;
        }

        /// <summary>
        /// Set culture for thread by culture name
        /// </summary>
        /// <param name="cultureName"></param>
        public static void SetCultureForThread(string cultureName)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }

        /// <summary>
        ///     Static constructor that contains registrations of supported cultures
        /// </summary>
        static CEngine()
        {
            RegisterCulture(DefaultCulture);
            RegisterCulture("en-US");
        }
    }
}
