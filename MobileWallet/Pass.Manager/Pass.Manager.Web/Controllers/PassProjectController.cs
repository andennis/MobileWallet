using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassProjectController : BaseEntityController<PassProjectViewModel, PassProject>
    {
        public PassProjectController(IPassProjectService passService)
            : base(passService)
        {
        }

        [HttpGet]
        public ActionResult CreateProject(int passSiteId)
        {
            return View("Create", new PassProjectViewModel() { PassSiteId = passSiteId });
        }

        public override ActionResult Create(PassProjectViewModel model)
        {
            model.RedirectUrl = Url.Action("Edit", "PassSite", new {id = model.PassSiteId});
            return base.Create(model);
        }

        public override ActionResult Edit(PassProjectViewModel model)
        {
            model.RedirectUrl = Url.Action("Edit", "PassSite", new { id = model.PassSiteId });
            return base.Edit(model);
        }
    }
}