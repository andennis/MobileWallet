using System.Web.Mvc;
using Common.Web;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Controllers
{
    public class PassContentTemplateController : BaseEntityController<PassContentTemplateViewModel, PassContentTemplate, IPassContentTemplateService, PassContentTemplateFilter>
    {
        public PassContentTemplateController(IPassContentTemplateService templateService)
            : base(templateService)
        {
        }

        [HttpGet]
        public ActionResult CreateContent(int passProjectId)
        {
            return Create(m => m.PassProjectId = passProjectId);
        }

        [ActionName("CreateContent")]
        public override ActionResult Create(PassContentTemplateViewModel model)
        {
            return base.Create(model);
        }

        protected override void SetDefaultReturnUrl(PassContentTemplateViewModel model)
        {
            base.SetDefaultReturnUrl(model);
            if (string.IsNullOrEmpty(model.RedirectUrl))
                model.RedirectUrl = Url.Action<PassProjectController>(a => a.Edit(null), new { id = model.PassProjectId });
        }

        [AjaxOnly]
        public ActionResult TabFields(int id)
        {
            return PartialView("Tabs/_Fields", id);
        }

        [AjaxOnly]
        public ActionResult TabBeacons(int id)
        {
            return PartialView("Tabs/_Beacons", id);
        }

        [AjaxOnly]
        public ActionResult TabLocations(int id)
        {
            return PartialView("Tabs/_Locations", id);
        }

        [AjaxOnly]
        public ActionResult TabImages(int id)
        {
            return PartialView("Tabs/_Images", id);
        }

    }
}