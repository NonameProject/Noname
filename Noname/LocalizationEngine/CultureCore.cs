using System.Collections.Generic;

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
            if (!SupportedCultures.Contains(cultureName))
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
            return SupportedCultures.Contains(cultureName);
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
