using System.Web.Mvc;
using System.Web.Routing;

namespace Pass.Distribution.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}"
                //defaults: new { controller = "Pass", action = "Index" }
            );

            routes.MapRoute(
                name: "Distribution1",
                url: "{controller}/{action}/{id}"
                //defaults: new { controller = "Pass", action = "Index" }
            );
            routes.MapRoute(
                name: "Distribution2",
                url: "{controller}/{action}/{token}"
                //defaults: new { controller = "Pass", action = "Index" }
            );
        }
    }
}