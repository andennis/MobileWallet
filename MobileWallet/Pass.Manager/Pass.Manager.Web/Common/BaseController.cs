using System;
using System.Web.Mvc;
using Common.BL;
using Common.Web.Controls.Grid;

namespace Pass.Manager.Web.Common
{
    public abstract class BaseController : Controller
    {
        protected virtual JsonResult JsonEx(bool success = true, string message = null)
        {
            return JsonEx(null, success, message);
        }
        protected virtual JsonResult JsonEx(object data, bool success = true, string message = null)
        {
            return JsonEx(data, JsonRequestBehavior.DenyGet, success, message);
        }
        public virtual JsonResult JsonEx(object data, JsonRequestBehavior behavior, bool success = true, string message = null)
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