﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.BL;
using Common.Repository;
using Common.Web;
using Pass.Distribution.Core.Entities;
using Pass.Distribution.Core.Services;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Controllers
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

        protected override void PrepareModelToEditView(PassContentViewModel model)
        {
            base.PrepareModelToEditView(model);
            string token = _distributionService.EncryptPassToken(new PassTokenInfo() {PassContentId = model.PassContentId});
            model.DistributionLink = _pmConfig.WebDistributionUrl.TrimEnd('/') + "/pass/e-t?token=" + HttpUtility.UrlEncode(token);
        }

        [AjaxOnly]
        public ActionResult TabFields(int id)
        {
            return PartialView(@"Tabs\_Fields", id);
        }

        [AjaxOnly]
        public ActionResult TabRegistrations(int id)
        {
            var statuses = new List<SelectListItem>()
                     {
                         new SelectListItem(){Value = ((int)EntityStatus.Active).ToString(), Text = Resources.Resources.EntityStatusActive},
                         new SelectListItem(){Value = ((int)EntityStatus.Inactive).ToString(), Text = Resources.Resources.EntityStatusInactive}
                     };

            ViewBag.RegStatses = new SelectList(statuses, "Value", "Text", ((int)EntityStatus.Active).ToString());
            return PartialView(@"Tabs\_Registrations", id);
        }

    }
}