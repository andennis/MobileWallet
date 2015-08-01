using System.Collections;
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
    public class PassContentTemplateFieldController : BaseEntityController<PassContentTemplateFieldViewModel, PassContentTemplateField, PassContentTemplateFieldView, IPassContentTemplateFieldService, PassContentTemplateFieldFilter>
    {
        public PassContentTemplateFieldController(IPassContentTemplateFieldService templateFieldService)
            : base(templateFieldService)
        {
        }

        [HttpGet]
        public ActionResult CreateField(int contentTemplateId)
        {
            //IEnumerable<PassProjectField> projectFields = _service.GetUnmappedFields(contentTemplateId);
            return Create(m =>
                    {
                        m.PassContentTemplateId = contentTemplateId;
                        //m.PassProjectFields = new SelectListTyped<PassProjectField, int, string>(projectFields, d => d.PassProjectFieldId, t => t.Name);
                    });

        }

        [ActionName("CreateField")]
        public override ActionResult Create(PassContentTemplateFieldViewModel model)
        {
            return base.Create(model);
        }

        protected override void PrepareModelToEditView(PassContentTemplateFieldViewModel model)
        {
            base.PrepareModelToEditView(model);
            PrepareModel(model);
        }

        protected override void PrepareModelToCreateView(PassContentTemplateFieldViewModel model)
        {
            base.PrepareModelToCreateView(model);
            PrepareModel(model);
        }

        private void PrepareModel(PassContentTemplateFieldViewModel model)
        {
            IEnumerable<PassProjectField> projectFields = _service.GetUnmappedFields(model.PassContentTemplateId, model.PassProjectFieldId);
            model.PassProjectFields = new SelectListTyped<PassProjectField, int, string>(projectFields, d => d.PassProjectFieldId, t => t.Name);
        }
    }
}