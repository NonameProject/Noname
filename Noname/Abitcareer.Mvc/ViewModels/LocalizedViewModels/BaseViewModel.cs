using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abitcareer.Mvc.ViewModels.LocalizedViewModels
{
    public class BaseViewModel
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string NameEN { get; set; }

        public string Title
        {
            get
            {
                if (Thread.CurrentThread.CurrentCulture.Name == "en-US")
                {
                    return NameEN;
                }
                return Name;
            }
        }
    }
}
