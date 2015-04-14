using System.Collections.Generic;
using System.Web.Mvc;
using Common.Web;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassProjectController : BaseEntityController<PassProjectViewModel, PassProject, IPassProjectService, PassProjectFilter>
    {
        private readonly IPassSiteCertificateService _siteCertificateService;

        public PassProjectController(IPassProjectService passService, IPassSiteCertificateService siteCertificateService)
            : base(passService)
        {
            _siteCertificateService = siteCertificateService;
        }

        [HttpGet]
        public ActionResult CreateProject(int passSiteId)
        {
            IEnumerable<PassCertificate> certificates = _siteCertificateService.GetCertificates(passSiteId);
            return Create(m =>
                          {
                              m.PassSiteId = passSiteId;
                              m.PassCertificates = new SelectListTyped<PassCertificate, int, string>(certificates, d => d.PassCertificateId, t => t.Name);
                          });
        }

        [ActionName("CreateProject")]
        public override ActionResult Create(PassProjectViewModel model)
        {
            return base.Create(model);
        }

        protected override void SetDefaultReturnUrl(IViewModel model)
        {
            base.SetDefaultReturnUrl(model);
            if (string.IsNullOrEmpty(model.RedirectUrl))
                model.RedirectUrl = Url.Action("Edit", "PassSite", new {id = ((PassProjectViewModel) model).PassSiteId});
        }

        protected override void PrepareModelToEditView(PassProjectViewModel model)
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
        public ActionResult TabContents(int id)
        {
            return PartialView("Tabs/_Contents", id);
        }

    }
}