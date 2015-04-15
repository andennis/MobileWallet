using System.Web.Mvc;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassContentController : BaseEntityController<PassContentViewModel, PassContent, IPassContentService, PassContentFilter>
    {
        public PassContentController(IPassContentService passService)
            : base(passService)
        {
        }

        [HttpGet]
        public ActionResult CreatePass(int contentTempleteId)
        {
            return Create(m => m.PassContentTemplateId = contentTempleteId);
        }

        [ActionName("CreatePass")]
        public override ActionResult Create(PassContentViewModel model)
        {
            return base.Create(model);
        }

    }
}