using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Models;
using Abitcareer.Mvc.ViewModels.Authorize;
using Abitcareer.NHibernateDataProvider.Data_Providers;
using CultureEngine;
using Events.Business.Components;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Abitcareer.Mvc.Controllers
{
    public class UserController : Controller
    {
        UserManager manager;

        public UserController(UserManager manager)
        {
            this.manager = manager;
        }


        [HttpGet]
        public ActionResult LogIn()
        {
            if (UserContext.Current.IsLoggedIn)
            {
                return RedirectToRoute("specialities");
            }
            return View();
        }

        
        [HttpPost]
        public ActionResult LogIn(UserViewModel user, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if(manager.IsPasswordValid(user.Email, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                if(!String.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToRoute("specialities");
            }
            else
            {
                ModelState.AddModelError("Password", LocalizationResx.LoginIncorrect);
            }

            return View(user);
        }


        [HttpGet]
        public ActionResult LogOut()
        {
            SignOut();
            return RedirectToRoute("Default");
        }

    
        private void SignOut()
        {
            FormsAuthentication.SignOut();

            if (Request.Cookies["user"] != null)
            {
                var user = new HttpCookie("user")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = null
                };
                Response.Cookies.Add(user);
            }
        }
    }
}
