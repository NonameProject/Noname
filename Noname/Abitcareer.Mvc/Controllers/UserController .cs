using Abitcareer.Business.Models;
using Abitcareer.Mvc.ViewModels.Authorize;
using Abitcareer.NHibernateDataProvider.Data_Providers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Abitcareer.Mvc.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserViewModel user)
        {
            
            return View();
        }
    }
}
