using Abitcareer.Business.Models;
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

        }
    }
}
