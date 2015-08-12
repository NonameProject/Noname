﻿using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Events.Business.Components
{
    public abstract class UserContext
    {
        public static UserContext Current
        {
            get
            {
                return DependencyResolver.Current.GetService<UserContext>();
            }
        }

        public abstract bool IsLoggedIn { get; }

        public abstract User User { get; }
    }
}