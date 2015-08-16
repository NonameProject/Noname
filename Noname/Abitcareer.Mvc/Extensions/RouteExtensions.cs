using System;
using System.Dynamic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Abitcareer.Mvc.Extensions
{
    public static class RouteExtensions
    {
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults, Action<dynamic> setupConstraints)
        {
            dynamic constraints = new ExpandoObject();

            constraints.locale = "[a-zA-Z]{2}-[a-zA-Z]{2}";

            setupConstraints(constraints);

            object constraintObject = (object)constraints;

            return routes.MapRoute(name, "{locale}/" + url, defaults, constraintObject).SetRouteName(name); ;
        }

        public static string GetRouteName(this Route route)
        {
            if (route == null)
            {
                return null;
            }

            return route.DataTokens.GetRouteName();
        }

        public static string GetRouteName(this RouteData routeData)
        {
            if (routeData == null)
            {
                return null;
            }

            return routeData.DataTokens.GetRouteName();
        }

        public static string GetRouteName(this RouteValueDictionary routeValues)
        {
            if (routeValues == null)
            {
                return null;
            }

            object routeName = null;

            routeValues.TryGetValue("__RouteName", out routeName);

            return routeName as string;
        }

        public static Route SetRouteName(this Route route, string routeName)
        {
            if (route == null)
            {
                throw new ArgumentNullException("route");
            }

            if (route.DataTokens == null)
            {
                route.DataTokens = new RouteValueDictionary();
            }

            route.DataTokens["__RouteName"] = routeName;

            return route;
        }
    }
}
