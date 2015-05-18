using System;
using System.Linq;
using System.Web.Mvc;
using Common.BL;
using Common.Web;
using Microsoft.Ajax.Utilities;
using Pass.Distribution.Core.Entities;
using Pass.Distribution.Core.Services;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassContentController : BaseEntityController<PassContentViewModel, PassContent, PassContentView, IPassContentService, PassContentFilter>
    {
        private readonly IPassContentTemplateService _contentTemplateService;
        private readonly IPassManagerConfig _pmConfig;
        private readonly IPassDistributionService _distributionService;

        public PassContentController(IPassManagerConfig pmConfig, 
            IPassContentService passService, 
            IPassContentTemplateService contentTemplateService,
            IPassDistributionService distributionService)
            : base(passService)
        {
            _pmConfig = pmConfig;
            _contentTemplateService = contentTemplateService;
            _distributionService = distributionService;
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

        protected override void PrepareModelToEditView(PassContentViewModel model)
        {
            base.PrepareModelToEditView(model);
            string token = _distributionService.EncryptPassToken(new PassTokenInfo() {PassContentId = model.PassContentId});
            model.DistributionLink = _pmConfig.WebDistributionUrl.TrimEnd('/') + "/pass/e-t?token=" + token;
        }

        [AjaxOnly]
        public ActionResult TabFields(int id)
        {
            return PartialView(@"Tabs\_Fields", id);
        }

        [AjaxOnly]
        public ActionResult TabRegistrations(int id)
        {
            PassContent pc = _service.Get(id);
            PassContentTemplate pct = _contentTemplateService.GetDetails(pc.PassContentTemplateId);
            return PartialView(@"Tabs\_Registrations", id);
        }

    }
}