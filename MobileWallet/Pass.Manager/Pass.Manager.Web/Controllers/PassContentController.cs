using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.BL;
using Common.Web;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassContentController : BaseEntityController<PassContentViewModel, PassContent, IPassContentService, PassContentFilter>
    {
        private readonly IPassContentTemplateService _contentTemplateService;

        public PassContentController(IPassContentService passService, IPassContentTemplateService contentTemplateService)
            : base(passService)
        {
            _contentTemplateService = contentTemplateService;
        }

        public ActionResult Passes(int passProjectId)
        {
            SearchResult<PassContentTemplate> contentTemplates = _contentTemplateService.Search(new SearchContext(), new PassContentTemplateFilter() {PassProjectId = passProjectId});
            var model = new PassContentListViewModel()
            {
                PassContentTemplateId = contentTemplates.Data.Count() == 1 ? contentTemplates.Data.First().PassContentTemplateId : (int?)null,
                PassContentTemplates = new SelectListTyped<PassContentTemplate, int, string>(contentTemplates.Data, d => d.PassContentTemplateId, t => t.Name)
            };
            return View(model);
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

        protected override void SetDefaultReturnUrl(IViewModel model)
        {
            base.SetDefaultReturnUrl(model);
            if (string.IsNullOrEmpty(model.RedirectUrl))
                model.RedirectUrl = Url.Action<PassProjectController>(a => a.Edit(0), new { id = ((PassContentViewModel)model).PassProjectId });
        }

    }
}