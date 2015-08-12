using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Models;
using Abitcareer.Core.CustomExceptions;
using Elmah;
using System.Web.Mvc;
using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;
using Abitcareer.Business.Components.Translation;
using System.Threading;
using Abitcareer.Business.Components.XmlServices;

namespace Abitcareer.Mvc.Controllers
{
    public class DataImportController : Controller
    {
        FacultyManager facultyManager;

        SpecialityManager specialityManager;

        IFacultiesToSpecialitiesDataProvider facToSpecProvider;

        UniversityManager universityManager;

        CityManager cityManager;

        RegionManager regionManager;

        public DataImportController(CityManager cityManager ,RegionManager regionManager,UniversityManager universityManager,FacultyManager facultyManager, SpecialityManager specialityManager,
            IFacultiesToSpecialitiesDataProvider facToSpecProvider)
        {
            this.facultyManager = facultyManager;

            this.specialityManager = specialityManager;

            this.facToSpecProvider = facToSpecProvider;

            this.regionManager = regionManager;

            this.cityManager = cityManager;

            this.universityManager = universityManager;
        }

        public void  Import()
        {
            var service = new DataImportXmlService(Server.MapPath("~/App_Data/Data.xml"));

            var importingData = service.Parse();

            foreach(var node in importingData)
            {
                regionManager.Create(node.Region);

                cityManager.Create(node.City);

                universityManager.Create(node.University);

                facultyManager.Create(node.Faculty);

                specialityManager.Create(node.Speciality);

                facToSpecProvider.Create(node.FacultyToSpeciality);
            }
        }
    }
}