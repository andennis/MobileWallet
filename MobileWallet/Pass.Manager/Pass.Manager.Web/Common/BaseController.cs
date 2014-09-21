using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pass.Manager.Web.Common
{
    public abstract class BaseController : Controller
    {
        protected ActionResult RedirectTo(IViewModel model)
        {
            if (model.RedirectUrl == null)
                return RedirectToAction("Index");

            return Redirect(model.RedirectUrl);
        }

    }
}