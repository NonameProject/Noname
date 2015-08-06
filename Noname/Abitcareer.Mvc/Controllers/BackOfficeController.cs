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
using System.Linq;

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

            var spec = specialityManager.GetList() as List<Speciality>;
            var tmp = spec.Where(x =>
                !string.IsNullOrEmpty(x.Name) && !string.IsNullOrEmpty(x.Id)).ToList();
            var result = AutoMapper.Mapper.Map<List<SpecialityViewModel>>(tmp);
            return View(result);
        }

        public ActionResult EditSpecialities(string id)
        {
            var model = specialityManager.GetById(id);
            return View(new SpecialityViewModel() { Name = model.Name, Id = model.Id });
        }

        [HttpPost]
        public ActionResult Save(SpecialityViewModel editedModel)
        {
            var mappedModel = new Speciality();//AutoMapper.Mapper.Map(editedModel, new Speciality());
            var result = specialityManager.TrySave(mappedModel);
            return Json(result);
        }

        public ActionResult EditPartial()
        {
            return PartialView();
        }
    }
}
