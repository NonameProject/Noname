using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Models;
using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using Abitcareer.NHibernateDataProvider.Data_Providers;
using CultureEngine;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Abitcareer.Mvc.Controllers
{
    public class LocalizationEngineController : Controller
    {
        UniversityManager universityManager;
        RegionManager regionManager;
        CityManager cityManager;
        FacultyManager facultyManager;
        SpecialityManager specialityManager;

        public LocalizationEngineController(UniversityManager universityManager, RegionManager regionManager,
            SpecialityManager specialityManager, CityManager  cityManager, FacultyManager facultyManager)
        {
            this.universityManager = universityManager;
            this.regionManager = regionManager;
            this.facultyManager = facultyManager;
            this.specialityManager = specialityManager;
            this.cityManager = cityManager;
        }

        public ActionResult ChangeCulture(string culture, string routeName)
        {
            return RedirectToRoute(routeName, new { locale = culture });
        }

        private string Make(string name)
        {
            return "<uk>_<" + name + ">";
        }

        public ActionResult TestDb()
        {
            var doc = XDocument.Load(Server.MapPath("~/App_Data/Data.xml"));

            var regionModel = new Region();

            var cityModel = new City();

            var universityModel = new University();

            var facultyModel = new Faculty();

            var specialityModel = new Speciality();

           // ///test///

           //// regionModel.Id = 1;
           // regionModel.Name = "testreg";
           // regionModel.Fields.Add(Make(regionModel.Name), "<qweqweqeqeq>");

           //             //cityModel.Id = 1;
           // cityModel.Name = "testcity";
           // cityModel.Region = regionModel;
           // cityModel.Fields.Add(Make(cityModel.Name), "<12313>");

           // universityModel.City = cityModel;
           // //universityModel.Id = 1;
           // universityModel.Name = "testuniver";
           // universityModel.Fields.Add(Make(universityModel.Name), "<12313123>");


           // //facultyModel.Id = 1;
           // facultyModel.Name = "testfac";
           // facultyModel.Fields.Add(Make(facultyModel.Name), "<123123123>");


           // //specialityModel.Id = 1;
           // specialityModel.Name = "testspec";
           // specialityModel.Fields.Add(specialityModel.Name, "<123123>");

           // specialityModel.Faculties.Add(facultyModel);

           // facultyModel.Specialities.Add(specialityModel);
           // facultyModel.University = universityModel;
            

           // universityModel.Faculties.Add(facultyModel);
           // universityModel.City = cityModel;

           // cityModel.Universities.Add(universityModel);
           // cityModel.RegionId = regionModel.Id;

           // regionModel.Cities.Add(cityModel);

           // regionManager.Create(regionModel);
           // cityManager.Create(cityModel);
           // universityManager.Create(universityModel);

            ///test///

            
            //foreach(XElement region in doc.Root.Elements())
            //{
            //    regionModel.Id = Convert.ToInt32(region.Attribute("id").Value);
            //    regionModel.Name = region.Attribute("name").Value;

            //    foreach(XElement city in region.Elements())
            //    {
            //        cityModel.Id = Convert.ToInt32(city.Attribute("id").Value);
            //        cityModel.Name = city.Attribute("name").Value;

            //        foreach(XElement university in city.Elements())
            //        {
            //            universityModel.Id = Convert.ToInt32(university.Attribute("id").Value);
            //            universityModel.Name = university.Attribute("name").Value;
                        
            //            foreach(XElement faculty in university.Elements())
            //            {
            //                facultyModel.Id = Convert.ToInt32(faculty.Attribute("id").Value);
            //                facultyModel.Name = faculty.Attribute("name").Value;

            //                foreach(XElement speciality in faculty.Elements())
            //                {
            //                    specialityModel.Id = Convert.ToInt32(speciality.Attribute("id").Value);
            //                    specialityModel.Name = speciality.Attribute("name").Value;
            //                }
            //            }
            //        }
            //    }
            //}

            var list = AutoMapper.Mapper.Map<List<UniversityViewModel>>(universityManager.GetList());
            return View(list);
        }

        public ActionResult Import()
        {
            return View();
        }
    }
}
