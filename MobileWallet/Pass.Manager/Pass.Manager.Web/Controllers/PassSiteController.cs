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
    }
}