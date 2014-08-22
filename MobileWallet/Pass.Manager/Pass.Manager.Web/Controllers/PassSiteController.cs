using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassSiteController : Controller
    {
        public ActionResult Index()
        {
            return View(new PassSiteViewModel());
        }

        public ActionResult AjaxHandler()
        {
            return Json(new
            {
                sEcho = 1,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = new List<string[]>() {
                    new string[] {"1", "Microsoft", "Redmond", "USA"},
                    new string[] {"2", "Google", "Mountain View", "USA"},
                    new string[] {"3", "Gowi", "Pancevo", "Serbia"}
                    }
            },
            JsonRequestBehavior.AllowGet);
        }
    }
}