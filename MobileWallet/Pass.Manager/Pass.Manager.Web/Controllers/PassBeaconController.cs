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
        public PassBeaconController(IPassBeaconService imageService)
            : base(imageService)
        {
        }

        //[HttpGet]
        //public ActionResult CreateBeacon(int contentTemplateId)
        //{
        //    IEnumerable<PassProjectField> projectFields = _service.GetUnmappedFields(contentTemplateId);
        //    return Create(m =>
        //    {
        //        m.PassContentTemplateId = contentTemplateId;
        //        m.PassProjectFields = new SelectListTyped<PassProjectField, int, string>(projectFields, d => d.PassProjectFieldId, t => t.Name);
        //    });

        //}

        //[ActionName("CreateField")]
        //public override ActionResult Create(PassContentTemplateFieldViewModel model)
        //{
        //    return base.Create(model);
        //}
	}
}