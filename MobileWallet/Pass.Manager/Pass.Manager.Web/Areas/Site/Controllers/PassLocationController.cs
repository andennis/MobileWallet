using System.Web.Mvc;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Controllers
{
    public class PassLocationController : BaseEntityController<PassLocationViewModel, PassLocation, IPassLocationService, PassLocationFilter>
    {
        public PassLocationController(IPassLocationService locationService)
            : base(locationService)
        {
        }

        [HttpGet]
        public ActionResult CreateLocation(int contentTemplateId)
        {
            return Create(m => m.PassContentTemplateId = contentTemplateId);

        }

        [ActionName("CreateLocation")]
        public override ActionResult Create(PassLocationViewModel model)
        {
            return base.Create(model);
        }
    }
}