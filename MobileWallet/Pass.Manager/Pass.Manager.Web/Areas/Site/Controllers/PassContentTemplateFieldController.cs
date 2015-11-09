using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Common.Web;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Controllers
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
            return Create(m =>
                    {
                        m.PassContentTemplateId = contentTemplateId;
                    });
        }

        [ActionName("CreateField")]
        public override ActionResult Create(PassContentTemplateFieldViewModel model)
        {
            return base.Create(model);
        }

        protected override void PrepareModelToCreateView(PassContentTemplateFieldViewModel model)
        {
            base.PrepareModelToCreateView(model);
            IEnumerable<PassProjectField> projectFields = _service.GetUnmappedFields(model.PassContentTemplateId);
            model.PassProjectFields = new SelectListTyped<PassProjectField, int, string>(projectFields, d => d.PassProjectFieldId, t => t.Name);
        }

        protected override PassContentTemplateFieldViewModel GetViewModel(int entityId)
        {
            var entity = _service.GetView<PassContentTemplateFieldView>(entityId);
            return Mapper.Map<PassContentTemplateFieldView, PassContentTemplateFieldViewModel>(entity);
        }
    }
}