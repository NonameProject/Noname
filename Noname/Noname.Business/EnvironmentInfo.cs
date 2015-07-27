using System.Configuration;

namespace Abitcareer.Business.Components
{
    public class EnvironmentInfo
    {
        private static EnvironmentInfo instance;

        private EnvironmentInfo() {}

        public static EnvironmentInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnvironmentInfo();
                }
                return instance;
            }
        }

        public string Host
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Host");
            }
        }
        public string Email
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Email");
            }
        }

        public string ReminderCacheName
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("ReminderCacheName");
            }
        }
    }
}
