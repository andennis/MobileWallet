﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Web.Grid;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassSiteController : Controller
    {
        public ActionResult Index()
        {
            return View(new PassSiteViewModel());
        }

        private class MyClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Town { get; set; }
        }

        [AjaxOnly]
        public ActionResult AjaxHandler2(GridDataRequest request)
        {
            var data = new List<PassSiteViewModel>()
                       {
                           new PassSiteViewModel(){ PassSiteId = 1,  Name = "Microsoft", Description= "Redmond"},
                           new PassSiteViewModel(){PassSiteId = 2, Name = "Google", Description = "Mountain View"},
                           new PassSiteViewModel() { PassSiteId = 3, Name = "Gowi", Description = "Pancevo"}
                       };
            return Json(GridDataResponse.Create(request, data, 3), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult AjaxHandler(GridDataRequest request)
        {
            var data = new List<MyClass>()
                       {
                           new MyClass(){ Id = 1,  Name = "Microsoft", Address = "Redmond", Town = "USA"},
                           new MyClass(){Id = 2, Name = "Google", Address = "Mountain View", Town = "USA"},
                           new MyClass() { Id = 3, Name = "Gowi", Address = "Pancevo", Town = "Serbia"}
                       };
            return Json(GridDataResponse.Create(request, data, 3), JsonRequestBehavior.AllowGet);
        }
    }
}