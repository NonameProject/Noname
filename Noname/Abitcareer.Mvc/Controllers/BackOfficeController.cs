using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Models;
using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using Abitcareer.NHibernateDataProvider.Data_Providers;
using CultureEngine;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;
using Abitcareer.Business.Components.Translation;
using System.Threading;


namespace Abitcareer.Web.Components
{
    public class BackOfficeController : Controller
    {
        SpecialityManager specialityManager;

        public BackOfficeController(SpecialityManager manager)
        {
            specialityManager = manager;
        }

        public ActionResult Specialities()
        {
           // var res = new List<Speciality>();

            var spec = specialityManager.GetList();

            var result = AutoMapper.Mapper.Map(spec, new List<SpecialityViewModel>());

            return View(result);
        }

        public ActionResult EditSpecialities()
        {
      
            return View(new SpecialityViewModel());
        }

        [HttpPost]
        public ActionResult Save(SpecialityViewModel editedModel)
        {
            var mappedModel = new Speciality();//AutoMapper.Mapper.Map(editedModel, new Speciality());
            var result = specialityManager.TrySave(mappedModel);
            return Json(result);
        }
    }
}
