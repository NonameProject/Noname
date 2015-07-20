using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Components
{
    public static class EnvironmentInfo
    {
        public static string Host
        {
            get
            {
                return "http://noname.azurewebsites.net";
            }
            private set
            {
            }
        }
        public static string Email
        {
            get
            {
                return "noname@gmail.com";
            }
            private set
            {
            }
        }

        public static string ReminderCacheName
        {
            get
            {
                return "Reminder";
            }
            private set
            {
            }
        }
    }
}
