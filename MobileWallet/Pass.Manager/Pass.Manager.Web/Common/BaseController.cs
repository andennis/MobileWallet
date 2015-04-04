using System.Web.Mvc;

namespace Pass.Manager.Web.Common
{
    public abstract class BaseController : Controller
    {
        protected void SetDefaultReturnUrl(IViewModel model)
        {
            if (model.RedirectUrl == null && Request.UrlReferrer != null)
                model.RedirectUrl = Request.UrlReferrer.ToString();
        }

        protected virtual ActionResult RedirectTo(IViewModel model)
        {
            if (model.RedirectUrl == null)
                return RedirectToAction("Index");

            return Redirect(model.RedirectUrl);
        }

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
    }
}