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

        IFacultyToSpecialityDataProvider facToSpecProvider;

        UniversityManager universityManager;

        CityManager cityManager;

        RegionManager regionManager;

        public DataImportController(CityManager cityManager, RegionManager regionManager, UniversityManager universityManager, FacultyManager facultyManager, SpecialityManager specialityManager,
            IFacultyToSpecialityDataProvider facToSpecProvider)
        {
            this.facultyManager = facultyManager;

            this.specialityManager = specialityManager;

            this.facToSpecProvider = facToSpecProvider;

            this.regionManager = regionManager;

            this.cityManager = cityManager;

            this.universityManager = universityManager;
        }

        public void Import()
        {
            var service = new DataImportXmlService(Server.MapPath("~/App_Data/Data.xml"));

            var importingData = service.Parse();

            var specialityList = new List<string>();

            var facultyList = new List<string>();

            foreach (var node in importingData)
            {
                regionManager.Create(node.Region);

                cityManager.Create(node.City);

                universityManager.Create(node.University);

                if (!string.IsNullOrEmpty(node.Faculty.Name) && !facultyList.Contains(node.Faculty.Name))
                {
                    facultyManager.Create(node.Faculty);

                    facultyList.Add(node.Faculty.Name);
                }

                if (!string.IsNullOrEmpty(node.Speciality.Name) && !specialityList.Contains(node.Speciality.Name))
                {
                    specialityManager.Create(node.Speciality);

                    specialityList.Add(node.Speciality.Name);
                }

            }

            foreach (var node in importingData)
            {
                var facultyToSpeciality = new FacultyToSpeciality();

                var spec = specialityManager.GetByName(node.Speciality.Name);

                var fac = facultyManager.GetByName(node.Faculty.Name);

                if (fac != null && spec != null)
                {
                    facultyToSpeciality.Faculty = facultyManager.GetByName(node.Faculty.Name);

                    facultyToSpeciality.Speciality = specialityManager.GetByName(node.Speciality.Name);

                    facToSpecProvider.Create(facultyToSpeciality);
                }
            }
        }
    }
}