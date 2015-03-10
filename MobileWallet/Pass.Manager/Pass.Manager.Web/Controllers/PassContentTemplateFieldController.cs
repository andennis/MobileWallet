using System.Web.Mvc;
using Common.Web;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassContentTemplateFieldController : BaseEntityController<PassContentTemplateFieldViewModel, PassContentTemplateField, IPassContentTemplateFieldService, PassContentTemplateFieldFilter>
    {
        private readonly IPassContentTemplateService _templateService;

        public PassContentTemplateFieldController(IPassContentTemplateFieldService templateFieldService, IPassContentTemplateService templateService)
            : base(templateFieldService)
        {
            _templateService = templateService;
        }

        [HttpGet]
        public ActionResult CreateField(int contentTemplateId)
        {
            //PassContentTemplate template = _templateService.Get(contentTemplateId);
            return Create(m =>
                    {
                        m.PassContentTemplateId = contentTemplateId;
                        m.PassProjectFields = new SelectListTyped<PassProjectField, int, string>(new PassProjectField[0], d => d.PassProjectFieldId, t => t.Name);
                    });

        }

        [ActionName("CreateField")]
        public override ActionResult Create(PassContentTemplateFieldViewModel model)
        {
            return base.Create(model);
        }

    }
}