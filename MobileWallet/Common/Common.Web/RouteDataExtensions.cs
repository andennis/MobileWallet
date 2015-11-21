using System.Web.Routing;

namespace Common.Web
{
    public static class RouteDataExtensions
    {
        private const string PrmArea = "area";
        private const string PrmController = "controller";
        private const string PrmAction = "action";

        public static string GetArea(this RouteData routeData)
        {
            return routeData.DataTokens[PrmArea] as string;
        }
        public static void SetArea(this RouteData routeData, string areaName)
        {
            routeData.DataTokens[PrmArea] = areaName;
        }

        public static string GetController(this RouteData routeData)
        {
            return routeData.GetRequiredString(PrmController);
        }
        public static void SetController(this RouteData routeData, string controllerName)
        {
            routeData.SetRouteValue(PrmController, controllerName);
        }

        public static string GetAction(this RouteData routeData)
        {
            return routeData.GetRequiredString(PrmAction);
        }
        public static void SetAction(this RouteData routeData, string actionName)
        {
            routeData.SetRouteValue(PrmAction, actionName);
        }

        public static TReturn GetRouteValue<TReturn>(this RouteData routeData, string name)
        {
            return (TReturn)routeData.Values[name];
        }
        public static void SetRouteValue(this RouteData routeData, string name, object val)
        {
            routeData.Values[name] = val;
        }
    }
}