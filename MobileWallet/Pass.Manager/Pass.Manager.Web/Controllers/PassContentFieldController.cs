using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using AutoMapper;
using Pass.Container.Core.Exceptions;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassContentFieldController : BaseEntityController<PassContentFieldViewModel, PassContentField, PassContentFieldView, IPassContentFieldService, PassContentFieldFilter>
    {
        public PassContentFieldController(IPassContentFieldService fieldService)
            : base(fieldService)
        {
        }

        public ActionResult CreateContentField(int passContentId, int passProjectFieldId)
        {
            /*
            PassContentFieldView entityView = _service.GetView(passContentId, passProjectFieldId);

            if (entityView.PassContentFieldId.HasValue)
                return RedirectToAction("Edit", new {id = entityView.PassContentFieldId});
            */

            return Create(x => { 
                x.PassContentId = passContentId;
                x.PassProjectFieldId = passProjectFieldId;
            });
        }

        [ActionName("CreateContentField")]
        public override ActionResult Create(PassContentFieldViewModel model)
        {
            return base.Create(model);
        }

        protected override void PrepareModelToCreateView(PassContentFieldViewModel model)
        {
            PassContentFieldView entityView = _service.GetView(model.PassContentId, model.PassProjectFieldId);
            Mapper.Map<PassContentFieldView, PassContentFieldViewModel>(entityView, model);
        }

        protected override void PrepareModelToEditView(PassContentFieldViewModel model)
        {
            if (!model.PassContentFieldId.HasValue)
                throw new PassGenerationException("PassContentFieldId should be specified");

            var entityView = _service.GetView<PassContentFieldView>(model.PassContentFieldId.Value);
            Mapper.Map<PassContentFieldView, PassContentFieldViewModel>(entityView, model);
        }

        protected override PassContentFieldViewModel GetViewModel(int entityId)
        {
            var entity = _service.GetView<PassContentFieldView>(entityId);
            return Mapper.Map<PassContentFieldView, PassContentFieldViewModel>(entity);
        }
    }
}