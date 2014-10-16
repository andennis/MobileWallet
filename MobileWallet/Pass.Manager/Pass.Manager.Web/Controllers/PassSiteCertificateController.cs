using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Common.Web;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassSiteCertificateController : BaseEntityController<PassSiteCertificateViewModel, PassSiteCertificate, IPassSiteCertificateService, PassSiteCertificateFilter>
    {
        private readonly IPassCertificateService _certificateService;

        public PassSiteCertificateController(IPassSiteCertificateService siteCertificateService, IPassCertificateService certificateService)
            : base(siteCertificateService)
        {
            _certificateService = certificateService;
        }

        public ActionResult AddCertificate(int passSiteId)
        {
            var model = new PassSiteCertificateViewModel() { PassSiteId = passSiteId };
            SetDefaultReturnUrl(model);
            model.Certificates = new SelectListTyped<PassCertificate, int, string>(_service.GetUnassignedCertificates(passSiteId), x => x.PassCertificateId, x => x.Name);
            return View("Create", model);
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
    }
}