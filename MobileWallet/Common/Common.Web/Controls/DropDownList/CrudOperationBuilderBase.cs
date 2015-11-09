using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Common.Web.Controls.DropDownList
{
    public abstract class CrudOperationBuilderBase<TCrudOperationBuilder>
    {
        public readonly CrudOperation _operation;
        //protected readonly IUrlGenerator _urlGenerator;
        protected readonly ViewContext _viewContext;

        //public CrudOperationBuilderBase(CrudOperation operation, ViewContext viewContext, IUrlGenerator urlGenerator)
        protected CrudOperationBuilderBase(CrudOperation operation, ViewContext viewContext)
        {
            _operation = operation;
            _viewContext = viewContext;
        }

        public TCrudOperationBuilder Action(string actionName, string controllerName)
        {
            return Action(actionName, controllerName, null);
        }

        public TCrudOperationBuilder Action(string actionName, string controllerName, object routeValues)
        {
            _operation.ControllerName = controllerName;
            _operation.ActionName = actionName;
            _operation.RouteValues = new RouteValueDictionary(routeValues);
            return (TCrudOperationBuilder)Convert.ChangeType(this, typeof(TCrudOperationBuilder));
        }

        //
        // Summary:
        //     Sets the action, contoller and route values for the operation.
        //
        // Parameters:
        //   actionName:
        //     Action name
        //
        //   controllerName:
        //     Controller name
        //
        //   routeValues:
        //     Route values
        //public TCrudOperationBuilder Action(string actionName, string controllerName, object routeValues);
        ////
        //// Summary:
        ////     Sets the action, contoller and route values for the operation.
        ////
        //// Parameters:
        ////   actionName:
        ////     Action name
        ////
        ////   controllerName:
        ////     Controller name
        ////
        ////   routeValues:
        ////     Route values
        //public TCrudOperationBuilder Action(string actionName, string controllerName, RouteValueDictionary routeValues);
        ////
        //// Summary:
        ////     Sets JavaScript function which to return additional parameters which to be
        ////     sent the server.
        //public TCrudOperationBuilder Data(Func<object, object> handler);
        ////
        //// Summary:
        ////     Sets JavaScript function which to return additional parameters which to be
        ////     sent the server.
        ////
        //// Parameters:
        ////   handler:
        ////     JavaScript function name
        //public TCrudOperationBuilder Data(string handler);
        ////
        //// Summary:
        ////     Sets the route values for the operation.
        ////
        //// Parameters:
        ////   routeValues:
        ////     Route values
        //public TCrudOperationBuilder Route(RouteValueDictionary routeValues);
        ////
        //// Summary:
        ////     Sets the route name for the operation.
        ////
        //// Parameters:
        ////   routeName:
        //public TCrudOperationBuilder Route(string routeName);
        ////
        //// Summary:
        ////     Sets the route name and values for the operation.
        ////
        //// Parameters:
        ////   routeName:
        ////     Route name
        ////
        ////   routeValues:
        ////     Route values
        //public TCrudOperationBuilder Route(string routeName, object routeValues);
        ////
        //// Summary:
        ////     Sets the route name and values for the operation.
        ////
        //// Parameters:
        ////   routeName:
        ////     Route name
        ////
        ////   routeValues:
        ////     Route values
        //public TCrudOperationBuilder Route(string routeName, RouteValueDictionary routeValues);
        ////
        //// Summary:
        ////     Specifies the HTTP verb of the request.
        ////
        //// Parameters:
        ////   verb:
        ////     The HTTP verb
        //public TCrudOperationBuilder Type(HttpVerbs verb);
        ////
        //// Summary:
        ////     Specifies an absolute or relative URL for the operation.
        ////
        //// Parameters:
        ////   url:
        ////     Absolute or relative URL for the operation
        //public TCrudOperationBuilder Url(string url);
    }
}
