using System.Configuration;

namespace Abitcareer.Business.Components
{
    public static class EnvironmentInfo
    {
        public static string Host
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Host");
            }
        }
        public static string Email
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Email");
            }
        }

        public static string ReminderCacheName
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("ReminderCacheName");
            }
        }
    }
}
