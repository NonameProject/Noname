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
                return "http://abitcareer.azurewebsites.net";
            }
        }
        public static string Email
        {
            get
            {
                return "abitcareer@gmail.com";
            }
        }

        public static string ReminderCacheName
        {
            get
            {
                return "Reminder";
            }
        }
    }
}
