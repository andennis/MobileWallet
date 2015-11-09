using System;
using System.Web.Mvc;
using Common.BL;
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
                PageIndex = request.start,
                PageSize = request.length,
                SortBy = request.sortColumn,
                SortDirection = request.sortDirection
            };
        }

    }
}