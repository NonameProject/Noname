using Abitcareer.Business.Models;
using Abitcareer.Mvc.ViewModels.Authorize;
using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Mvc.App_Start
{
    public class AutomapperConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper
                .CreateMap<University, UniversityViewModel>()
                .IncludeBase<BaseModel, BaseViewModel>();
            AutoMapper.Mapper
                .CreateMap<UniversityViewModel, University>()
                .IncludeBase<BaseViewModel, BaseModel>();
            AutoMapper.Mapper
                .CreateMap<Speciality, SpecialityViewModel>()
                .IncludeBase<BaseModel,BaseViewModel>();
            AutoMapper.Mapper
                .CreateMap<SpecialityViewModel, Speciality>()
                .IncludeBase<BaseViewModel, BaseModel>();
            AutoMapper.Mapper
                .CreateMap<User, UserViewModel>();
            AutoMapper.Mapper
                .CreateMap<SpecialityViewModel, SpecialityAdvancedViewModel>();
            AutoMapper.Mapper
                .CreateMap<SpecialityAdvancedViewModel, SpecialityViewModel>();
            AutoMapper.Mapper
                .CreateMap<SpecialityAdvancedViewModel, Speciality>()
                .IncludeBase<BaseViewModel, BaseModel>();
            AutoMapper.Mapper
                .CreateMap<Speciality, SpecialityAdvancedViewModel>()
                .IncludeBase<BaseModel, BaseViewModel>();

            AutoMapper.Mapper
                .CreateMap<UserViewModel, User>();
        }
    }
}
