using Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abitcareer.Mvc
{
    public static class Cultures
    {
        public static void RegisterSupportedCultures()
        {
            SupportedCultures.DefaultCulture = "uk-UA";
            SupportedCultures.AddLocalization(SupportedCultures.DefaultCulture);
            SupportedCultures.AddLocalization("en-US");
            SupportedCultures.AddLocalization("ru-RU");
        }
    }
}