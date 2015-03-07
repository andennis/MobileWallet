using System.Web.Mvc;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassProjectFieldController : BaseEntityController<PassProjectFieldViewModel, PassProjectField, IPassProjectFieldService, PassProjectFieldFilter>
    {
        public PassProjectFieldController(IPassProjectFieldService fieldService)
            : base(fieldService)
        {
        }

        public ActionResult CreateField(int passProjectId)
        {
            //ViewBag.PassProjectId = passProjectId;
            //return Create();

            return Create(m => m.PassProjectId = passProjectId);
        }

        /*
        protected override void PrepareModelToCreateView(PassProjectFieldViewModel model)
        {
            base.PrepareModelToCreateView(model);
            if (ViewBag.PassProjectId != null)
                model.PassProjectId = ViewBag.PassProjectId;
        }
        */

        [ActionName("CreateField")]
        public override ActionResult Create(PassProjectFieldViewModel model)
        {
            return base.Create(model);
        }
    }
}