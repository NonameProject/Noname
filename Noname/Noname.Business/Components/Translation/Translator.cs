using System.Net;
using System.Text;
using System.Web;

namespace Abitcareer.Business.Components.Translation
{
    /// <summary>Provide translation methods</summary>
    public static class Translator
    {
        /// <summary>Translate text by using Google translate in UTF8.</summary>
        /// <param name="input">The input string.</param>
        /// <param name="from">Source lang</param>
        /// <param name="to">Destination lang</param>
        /// <returns>Translated string in UTF8</returns>
        public static string Translate(string input, Languages from, Languages to)
        {
            return Translate(input, from, to, Encoding.UTF8);
        }

        /// <summary>Translate text by using Google translate in indicated encoding.</summary>
        /// <param name="input">The input string.</param>
        /// <param name="from">Source lang</param>
        /// <param name="to">Destination lang</param>
        /// <returns>Translated string in indicated encoding</returns>
        public static string Translate(string input, Languages from, Languages to, Encoding encoding)
        {
            string url = string.Format("https://translate.google.com/translate_a/single?client=t&sl={0}&tl={1}&hl=en&dt=bd&dt=ex&dt=ld&dt=md&dt=qc&dt=rw&dt=rm&dt=ss&dt=t&dt=at&ie=UTF-8&oe=UTF-8&source=btn&ssel=0&tsel=0&kc=0&q={2}",
                                                from.ToString("g"),
                                                to.ToString("g"),
                                                HttpUtility.UrlEncode(input));
            string result = null;
            using (WebClient web = new WebClient())
            {
                web.Encoding = encoding;
                web.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                result = web.DownloadString(url);
            }
            return result.Split('[')[3].Split(',')[0];
        }
    }
}
