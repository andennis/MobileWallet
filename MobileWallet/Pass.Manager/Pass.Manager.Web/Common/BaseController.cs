using System;
using System.Web.Mvc;
using System.Web.Routing;
using Common.BL;
using Common.Logging;
using Common.Web;
using Common.Web.Controls.Grid;
using Microsoft.Practices.Unity;
using Pass.Manager.Core.Exceptions;
using Pass.Manager.Web.Areas.Site;

namespace Pass.Manager.Web.Common
{
    public abstract class BaseController : Controller
    {
        [Dependency]
        public UserContext<UserContextData> AuthUserContext { get; set; }
        
        [Dependency]
        public ILogger Logger { get; set; }

        protected override void Initialize(RequestContext context)
        {
            Logger.Debug(String.Format("Invoke action '{0}' from controller '{1}'.",
                context.HttpContext.Request.RequestContext.RouteData.GetRequiredString("action"),
                context.HttpContext.Request.RequestContext.RouteData.GetRequiredString("controller")));

            base.Initialize(context);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Logger.Error(filterContext.Exception, "Action '{0}' from controller '{1}' threw exception: '{2}'",
                filterContext.RouteData.Values["action"].ToString(),
                filterContext.RouteData.Values["controller"].ToString(),
                filterContext.Exception.Message);

            base.OnException(filterContext);
        }

        protected int SiteId
        {
            get
            {
                int siteId;
                if (!int.TryParse(RouteData.Values[SiteAreaRegistration.UrlPrmPassSiteId] as string, out siteId))
                    throw new PassManagerGeneralException(string.Format("URL does not contain the section '{0}'", SiteAreaRegistration.UrlPrmPassSiteId));

                return siteId;
            }
        }

        protected virtual string Layout { get { return null; } }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.Layout = Layout;
            base.OnActionExecuted(filterContext);
        }

        protected virtual JsonResult JsonEx(bool success = true, string message = null)
        {
            return JsonEx(null, success, message);
        }
        protected virtual JsonResult JsonEx(object data, bool success = true, string message = null)
        {
            return JsonEx(data, JsonRequestBehavior.DenyGet, success, message);
        }
        protected virtual JsonResult JsonEx(object data, JsonRequestBehavior behavior, bool success = true, string message = null)
        {
            var ajaxResp = new AjaxActionResponse()
                           {
                               Data = data,
                               Success = success,
                               Message = message
                           };
            return Json(ajaxResp, behavior);
        }

        protected SearchContext GridRequestToSearchContext(GridDataRequest request)
        {
            return new SearchContext
            {
                PageIndex = request.Start,
                PageSize = request.Length,
                SortBy = request.Order != null ? request.Columns[request.Order[0].Column].Data : null,
                SortDirection = request.Order != null ? request.Order[0].Dir : null
            };
        }

    }
}