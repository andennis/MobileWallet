using System.Web.Mvc;
using AutoMapper;
using Common.Web;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassSiteCertificateController : BaseEntityController<PassSiteCertificateViewModel, PassSiteCertificate, PassSiteCertificateView, IPassSiteCertificateService, PassSiteCertificateFilter>
    {
        private readonly IPassCertificateService _certificateService;

        public PassSiteCertificateController(IPassSiteCertificateService siteCertificateService, IPassCertificateService certificateService)
            : base(siteCertificateService)
        {
            _certificateService = certificateService;
        }

        public ActionResult AddCertificate(int passSiteId)
        {
            return Create(m => { m.PassSiteId = passSiteId; });
        }

        [ActionName("AddCertificate")]
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

        protected override void SetDefaultReturnUrl(PassSiteCertificateViewModel model)
        {
            base.SetDefaultReturnUrl(model);
            if (string.IsNullOrEmpty(model.RedirectUrl))
                model.RedirectUrl = Url.Action<PassSiteController>(a => a.Edit(0), new { id = model.PassSiteId });
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
            return RedirectToAction("Download", "PassCertificate", new {id = passSiteCertificate.PassCertificateId});
        }
        
        protected override void PrepareModelToCreateView(PassSiteCertificateViewModel model)
        {
            model.Certificates = new SelectListTyped<PassCertificate, int, string>(_service.GetUnassignedCertificates(model.PassSiteId), x => x.PassCertificateId, x => x.Name);
            base.PrepareModelToCreateView(model);
        }
        
    }
}