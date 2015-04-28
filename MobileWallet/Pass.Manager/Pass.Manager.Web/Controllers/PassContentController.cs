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
                PassProjectId = passProjectId,
                PassContentTemplateId = contentTemplates.Data.Any(x => x.IsDefault) ? contentTemplates.Data.First(x => x.IsDefault).PassContentTemplateId : (int?)null,
                PassContentTemplates = new SelectListTyped<PassContentTemplate, int, string>(contentTemplates.Data, d => d.PassContentTemplateId, t => t.Name)
            };
            return PartialView("_Passes", model);
        }

        [HttpGet]
        public ActionResult CreatePass(int passProjectId)
        {
            SearchResult<PassContentTemplate> contentTemplates = _contentTemplateService.Search(new SearchContext(), new PassContentTemplateFilter() { PassProjectId = passProjectId });
            return Create(m =>
                          {
                              m.PassContentTemplateId = contentTemplates.Data.Any(x => x.IsDefault) ? contentTemplates.Data.First(x => x.IsDefault).PassContentTemplateId : (int?) null;
                              m.PassContentTemplates = new SelectListTyped<PassContentTemplate, int, string>(contentTemplates.Data, d => d.PassContentTemplateId, t => t.Name);
                          });
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

        [AjaxOnly]
        public ActionResult TabFields(int id)
        {
            return PartialView(@"Tabs\_Fields", id);
        }
    }
}