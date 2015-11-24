using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Common.Logging;
using Common.Web;
using FluentValidation.Mvc;
using Microsoft.Practices.Unity;
using Pass.Manager.Web.Controllers;

namespace Pass.Manager.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.Configure();
            FluentValidationModelValidatorProvider.Configure();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            bool isAjaxCall = new HttpRequestWrapper(HttpContext.Current.Request).IsAjaxRequest();
            var container =  UnityConfig.GetConfiguredContainer();
            var logger = container.Resolve<ILogger>();
            RequestContext requestContext = ((MvcHandler)HttpContext.Current.CurrentHandler).RequestContext;
            logger.Error(exception, "Action '{0}' from controller '{1}' threw exception: '{2}'", 
                requestContext.RouteData.Values["action"],
                requestContext.RouteData.Values["controller"],
                exception.Message
                );
            Response.Clear();
            Server.ClearError();
            if (isAjaxCall)
            {
                Context.Response.ContentType = "application/json";
                Context.Response.StatusCode = 500;
                Context.Response.Write(
                    new JavaScriptSerializer().Serialize(
                        new
                        {
                            #if (DEBUG)
                                error = "Server Error in '/" + exception.Source + " Application.",
                                message = exception.Message,
                                stackTrace = "Stack Trace: " + exception.StackTrace
                            #elif (!DEBUG)
                                error = "Site error. The error message is already sent to developers."
                            #endif
                        })
                    );
            }
            else
            {
                var routeData = new RouteData();
                routeData.SetController("Error");
                #if (DEBUG)
                    routeData.SetAction("DebugGeneral");
                #elif (!DEBUG)
                    routeData.SetAction("General");
                #endif
                    routeData.SetRouteValue("exception", exception);

                Response.StatusCode = 500;
                Response.TrySkipIisCustomErrors = true;
                IController errorsController = new ErrorController();
                var wrapper = new HttpContextWrapper(Context);
                var rc = new RequestContext(wrapper, routeData);
                errorsController.Execute(rc);
            }
        }
    }
}
