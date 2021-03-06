﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;

namespace CultureEngine
{
    /// <summary>
    ///     Class which contains methods that provides culture actions: changing current culture, getting localized string etc.
    /// </summary>
    public class CEngine
    {
        private static CEngine instance;

        /// <summary>
        ///     Initialization method
        /// </summary>
        private CEngine()
        {
            RegisterCulture(DefaultCulture);
            RegisterCulture("uk");
            RegisterCulture("en-US");
        }

        /// <summary>
        ///    Realisation of Singleton pattern. Returns created instance of Engine. If instance not exist this one will create new. 
        /// </summary>
        public static CEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CEngine();
                }
                return instance;
            }
        }

        /// <summary>
        ///     List of supported cultures
        /// </summary>
        private List<string> SupportedCultures = new List<string>();

        /// <summary>
        /// Key for accessing stored cookies
        /// </summary>
        public readonly string CultureKey = "locale";

        /// <summary>
        ///     Default culture name
        /// </summary>
        public readonly string DefaultCulture = "uk-UA";

        /// <summary>
        ///     Method that creates resource manager for new culture and adds that culture to SupportedCultures list
        /// </summary>
        /// <param name="cultureName">Name of culture that will be registered</param>
        private void RegisterCulture(string cultureName)
        {
            if (!SupportedCultures.Any(s => s.Equals(cultureName, StringComparison.OrdinalIgnoreCase)))
            {
                SupportedCultures.Add(cultureName);
            }
        }

        /// <summary>
        /// Check is supported culture or not
        /// </summary>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        public bool IsSupported(string cultureName)
        {
            return SupportedCultures.Any(s => s.Equals(cultureName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Parse user languages array from request and return user's browser culture or default application culture name
        /// </summary>
        /// <param name="userLanguages"></param>
        /// <returns></returns>
        public string GetCultureByUserLanguages(string[] userRequestLanguages)
        {
            int userLanguagesLength = userRequestLanguages.Length;
            Char[] splitArray = new Char[] { ';' };
            for (int i = 0; i < userLanguagesLength; i++)
            {
                string culture = userRequestLanguages[i].Split(splitArray)[0];
                foreach (var item in SupportedCultures)
                {
                    if (item.IndexOf(culture, 0, StringComparison.OrdinalIgnoreCase) > 0)
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
        public void SetCultureForThread(string cultureName)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
    }
}
