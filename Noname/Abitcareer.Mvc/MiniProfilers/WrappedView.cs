using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Abitcareer.Mvc.MiniProfilers
{
    public class WrappedView : IView
    {
        IView wrapped;
        string name;
        bool isPartial;

        public WrappedView(IView wrapped, string name, bool isPartial)
        {
            this.wrapped = wrapped;
            this.name = name;
            this.isPartial = isPartial;
        }

        public void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            using (MiniProfiler.Current.Step("Render " + (isPartial ? "partial" : "") + ": " + name))
            {
                wrapped.Render(viewContext, writer);
            }
        }
    }
}
