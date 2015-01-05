using System.Web.Mvc;
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
            return PartialView("Field", new PassProjectFieldViewModel() { PassProjectId = passProjectId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateField(PassProjectFieldViewModel model)
        {
            if (ModelState.IsValid)
            {
                return JsonEx();
            }
            return PartialView("Field", model);
        }

        public ActionResult TestWindiw()
        {
            return Content("<b>Hello1</b>");
        }

        public ActionResult TestWindiw2()
        {
            return Content("<b>Hello2</b>");
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