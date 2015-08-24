using System.Web;
using System.Web.Routing;


    public class PersonImageRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new JsHttpHandler();
        }
    }
