using StudentList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentList.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)// кастомная модель
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email, model.Password))
                {

                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        return RedirectToAction("List", "Student");
                }
                else
                {
                    ModelState.AddModelError("", "Ви ввели невірний пароль");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

    }
}
