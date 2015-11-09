using System.Collections.Generic;
using System.Web.Mvc;
using Common.Web;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Controllers
{
    public class PassProjectController : BaseEntityController<PassProjectViewModel, PassProject, IPassProjectService, PassProjectFilter>
    {
        private readonly IPassSiteCertificateService _siteCertificateService;

        public PassProjectController(IPassProjectService passService, IPassSiteCertificateService siteCertificateService)
            : base(passService)
        {
            _siteCertificateService = siteCertificateService;
        }

        public override ActionResult Create()
        {
            IEnumerable<PassCertificate> certificates = _siteCertificateService.GetCertificates(SiteId);
            return Create(m =>
                          {
                              m.PassSiteId = SiteId;
                              m.PassCertificates = new SelectListTyped<PassCertificate, int, string>(certificates, d => d.PassCertificateId, t => t.Name);
                          });
        }

        protected override void PrepareModelToCreateView(PassProjectViewModel model)
        {
            PrepareModel(model);
        }
        protected override void PrepareModelToEditView(PassProjectViewModel model)
        {
            PrepareModel(model);
        }

        private void PrepareModel(PassProjectViewModel model)
        {
            IEnumerable<PassCertificate> certificates = _siteCertificateService.GetCertificates(model.PassSiteId);
            model.PassCertificates = new SelectListTyped<PassCertificate, int, string>(certificates, d => d.PassCertificateId, t => t.Name);
        }

        [AjaxOnly]
        public ActionResult TabFields(int id)
        {
            return PartialView("Tabs/_Fields", id);
        }

        [AjaxOnly]
        public ActionResult TabPassContentTemplates(int id)
        {
            return PartialView("Tabs/_PassContentTemplates", id);
        }

    }
}