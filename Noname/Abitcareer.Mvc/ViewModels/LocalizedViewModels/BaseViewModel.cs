using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Abitcareer.Mvc.ViewModels.LocalizedViewModels
{
    public class BaseViewModel
    {
        [ScaffoldColumn(false)]
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }
    }
}
