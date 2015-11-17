using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pass.Manager.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult General(Exception exception)
        {
            return View("Error", exception);
        }

        public ActionResult DebugGeneral(Exception exception)
        {
            return View("DebugError", exception);
        }

        //public ActionResult Http404()
        //{
        //    return View("404");
        //}

        //public ActionResult Http403()
        //{
        //    return View("403");
        //}

        //public ActionResult ExhibitNotFound()
        //{
        //    return View();
        //}
    }
}