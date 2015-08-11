using System;
using System.Web.Mvc;
using AutoMapper;
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

        public ActionResult CreateContentField(int? passContentId, int? passProjectFieldId)
        {
            if (!passContentId.HasValue)
                throw new ArgumentNullException("passContentId");
            if (!passProjectFieldId.HasValue)
                throw new ArgumentNullException("passProjectFieldId");

            PassContentFieldView entityView = _service.GetView(passContentId.Value, passProjectFieldId.Value);

            if (!entityView.PassContentFieldId.HasValue)
            {
                return Create(x => Mapper.Map(entityView, x));
            }

            return Edit(entityView.PassContentFieldId.Value);
        }

        [ActionName("CreateContentField")]
        public override ActionResult Create(PassContentFieldViewModel model)
        {
            return base.Create(model);
        }

        protected override PassContentFieldViewModel GetViewModel(int entityId)
        {
            var entity = _service.GetView<PassContentFieldView>(entityId);
            return Mapper.Map<PassContentFieldView, PassContentFieldViewModel>(entity);
        }
    }
}