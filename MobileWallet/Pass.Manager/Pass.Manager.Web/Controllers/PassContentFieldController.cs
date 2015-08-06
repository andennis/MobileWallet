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

        public ActionResult CreateOrEdit(int id, int? passContentId, int? passProjectFieldId)
        {
            if (id == 0)
            {
                if (!passContentId.HasValue)
                    throw new ArgumentNullException("passContentId");
                if (!passProjectFieldId.HasValue)
                    throw new ArgumentNullException("passProjectFieldId");

                return Create(x => {
                    x.PassContentId = passContentId.Value;
                    x.PassProjectFieldId = passProjectFieldId.Value;
                });
            }

            return Edit(id);
        }

        [HttpPost]
        public ActionResult CreateOrEdit(PassContentFieldViewModel model)
        {
            if (model.EntityId == 0)
                return Create(model);

            return Edit(model);
        }

        protected override PassContentFieldViewModel GetViewModel(int entityId)
        {
            var entity = _service.GetView<PassContentFieldView>(entityId);
            return Mapper.Map<PassContentFieldView, PassContentFieldViewModel>(entity);
        }
    }
}