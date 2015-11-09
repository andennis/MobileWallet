using System.Web.Mvc;
using AutoMapper;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Controllers
{
    public class PassContentFieldController : BaseEntityController<PassContentFieldViewModel, PassContentField, PassContentFieldView, IPassContentFieldService, PassContentFieldFilter>
    {
        public PassContentFieldController(IPassContentFieldService fieldService)
            : base(fieldService)
        {
        }

        public ActionResult CreateContentField(int passContentId, int passProjectFieldId)
        {
            PassContentFieldView entityView = _service.GetView(passContentId, passProjectFieldId);

            if (entityView.PassContentFieldId.HasValue)
                return RedirectToAction("Edit", new {id = entityView.PassContentFieldId});

            return Create(x => Mapper.Map(entityView, x));
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