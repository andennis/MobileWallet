using System.Collections.Generic;
using System.Web.Mvc;
using Common.Web;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassBeaconController : BaseEntityController<PassBeaconViewModel, PassBeacon, IPassBeaconService, PassBeaconFilter>
    
    {
        public PassBeaconController(IPassBeaconService beaconService)
            : base(beaconService)
        {
        }

        [HttpGet]
        public ActionResult CreateBeacon(int contentTemplateId)
        {
            return Create(m => m.PassContentTemplateId = contentTemplateId);

        }

        [ActionName("CreateBeacon")]
        public override ActionResult Create(PassBeaconViewModel model)
        {
            return base.Create(model);
        }
	}
}