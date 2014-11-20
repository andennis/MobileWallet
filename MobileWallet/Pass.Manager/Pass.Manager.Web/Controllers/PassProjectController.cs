﻿using System.Web.Mvc;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassProjectController : BaseEntityController<PassProjectViewModel, PassProject, IPassProjectService, PassProjectFilter>
    {
        public PassProjectController(IPassProjectService passService)
            : base(passService)
        {
        }

        [HttpGet]
        public ActionResult CreateProject(int passSiteId)
        {
            var model = new PassProjectViewModel() {PassSiteId = passSiteId};
            SetDefaultReturnUrl(model);
            return View("Create", model);
        }

        public ActionResult CreateField(int passProjectId)
        {
            return PartialView("FieldForm");
        }

        public ActionResult TestWindiw()
        {
            return Content("Hello");
        }

        /*
        public ActionResult PassDesigner(int passProjectId)
        {
            PassProject prj = _service.Get(passProjectId);
            //Do something
            return RedirectToAction("EmptyCardDesigner", "PassDesigner", new { passProjectId });
        }
        */

    }
}