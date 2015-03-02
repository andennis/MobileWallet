using System.Web.Mvc;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
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
            ViewBag.PassProjectId = passProjectId;
            return Create();
        }

        protected override void PrepareModelToCreateView(PassContentTemplateViewModel model)
        {
            base.PrepareModelToCreateView(model);
            if (ViewBag.PassProjectId != null)
                model.PassProjectId = ViewBag.PassProjectId;
        }

        [ActionName("CreateContent")]
        public override ActionResult Create(PassContentTemplateViewModel model)
        {
            return base.Create(model);
        }

    }
}