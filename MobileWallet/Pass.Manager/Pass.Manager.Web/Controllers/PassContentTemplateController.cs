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
            return Create(m => m.PassProjectId = passProjectId);
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


    }
}