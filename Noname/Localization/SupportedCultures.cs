using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localization
{
    public static class SupportedCultures
    {
        public static List<string> Cultures = new List<string>();

        public static string DefaultLocalization { get; set; }

        public static bool Exist(string cultureName)
        {
            return Cultures.Contains(cultureName);
        }

        public static void AddLocalization(string cultureName)
        {
            if (!Exist(cultureName))
            {
                Cultures.Add(cultureName);
            }
        }
    }
}
