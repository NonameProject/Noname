using StackExchange.Profiling;
using System.Web.Mvc;

namespace Abitcareer.Mvc.MiniProfilers
{
    public class ProfilingViewEngine : IViewEngine
    {
        IViewEngine wrapped;

        public ProfilingViewEngine(IViewEngine wrapped)
        {
            this.wrapped = wrapped;
        }

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var found = wrapped.FindPartialView(controllerContext, partialViewName, useCache);
            if (found != null && found.View != null)
            {
                found = new ViewEngineResult(new WrappedView(found.View, partialViewName, isPartial: true), this);
            }
            return found;
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var found = wrapped.FindView(controllerContext, viewName, masterName, useCache);
            if (found != null && found.View != null)
            {
                found = new ViewEngineResult(new WrappedView(found.View, viewName, isPartial: false), this);
            }
            return found;
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            wrapped.ReleaseView(controllerContext, view);
        }
    }
}
