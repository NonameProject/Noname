using Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noname.Mvc
{
    public static class Cultures
    {
        public static void RegisterSupportedCultures()
        {
            SupportedCultures.DefaultLocalization = "uk-UA";
            SupportedCultures.AddLocalization(SupportedCultures.DefaultLocalization);
            SupportedCultures.AddLocalization("en-US");
            SupportedCultures.AddLocalization("ru-RU");
        }
    }
}