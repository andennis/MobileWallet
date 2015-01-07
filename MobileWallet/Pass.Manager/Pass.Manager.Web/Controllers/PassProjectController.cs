using System.Web.Mvc;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
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

        public ActionResult TabFields(int id)
        {
            return PartialView("Tabs/_Fields", id);
        }

    }
}