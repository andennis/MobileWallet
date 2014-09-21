using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Common.Extensions;

namespace Common.Web
{
    public static class UrlHelperExtensions
    {
        public static string Action<TController>(this UrlHelper url, Expression<Func<TController, ActionResult>> expression, object routeValues = null)
        {
            string actionName = expression.GetMethodOrPropertyName();
            string typeName = typeof (TController).Name;
            if (!typeName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException(string.Format("Type '{0}' is not a controller", typeName));

            var controllerName = typeName.Substring(0, typeName.Length - "Controller".Length);
            if (controllerName.Length == 0)
                throw new ArgumentException(string.Format("Type '{0}' is not a controller", typeName));

            return url.Action(actionName, controllerName, routeValues);
        }
    }
}
