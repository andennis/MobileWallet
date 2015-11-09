using System.Web.Mvc;
using AutoMapper;
using Common.Web;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Controllers
{
    public class PassSiteCertificateController : BaseEntityController<PassSiteCertificateViewModel, PassSiteCertificate, PassSiteCertificateView, IPassSiteCertificateService, PassSiteCertificateFilter>
    {
        private readonly IPassCertificateService _certificateService;

        public PassSiteCertificateController(IPassSiteCertificateService siteCertificateService, IPassCertificateService certificateService)
            : base(siteCertificateService)
        {
            _certificateService = certificateService;
        }

        public override ActionResult Create()
        {
            return Create(m =>
            {
                m.PassSiteId = SiteId;
                m.Certificates = new SelectListTyped<PassCertificate, int, string>(_service.GetUnassignedCertificates(SiteId), x => x.PassCertificateId, x => x.Name);
            });
        }

        public override ActionResult Create(PassSiteCertificateViewModel model)
        {
            if (ModelState.IsValid)
            {
                PassSiteCertificate entity = Mapper.Map<PassSiteCertificateViewModel, PassSiteCertificate>(model);
                _service.Create(entity);

                PassCertificateApple certificate = _certificateService.Get(model.PassCertificateId);
                certificate = Mapper.Map<PassSiteCertificateViewModel, PassCertificateApple>(model, certificate);
                _certificateService.Update(certificate);
                model.Certificates = new SelectListTyped<PassCertificate, int, string>(_service.GetUnassignedCertificates(model.PassSiteId), x => x.PassCertificateId, x => x.Name);
                return RedirectTo(model);
            }

            return View(model);
        }
        
        public override ActionResult Edit(PassSiteCertificateViewModel model)
        {
            if (ModelState.IsValid)
            {
                PassSiteCertificate entity = _service.Get(model.EntityId);
                entity = Mapper.Map<PassSiteCertificateViewModel, PassSiteCertificate>(model, entity);
                entity.PassCertificate = Mapper.Map<PassSiteCertificateViewModel, PassCertificate>(model, entity.PassCertificate);
                _service.Update(entity);
                return RedirectTo(model);
            }

            return View(model);
        }

        public ActionResult Download(int id)
        {
            PassSiteCertificate passSiteCertificate = _service.Get(id);
            //TODO RedirectToAction should be a generic method
            return RedirectToAction("Download", "PassCertificate", new {id = passSiteCertificate.PassCertificateId, area = ""});
        }
        
    }
}