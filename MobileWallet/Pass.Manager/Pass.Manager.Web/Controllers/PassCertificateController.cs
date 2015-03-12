using System;
using System.IO;
using System.Web.Mvc;
using AutoMapper;
using Common.BL;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassCertificateController : BaseEntityController<PassCertificateAppleViewModel, PassCertificateApple, IPassCertificateService, SearchFilterBase>
    {
        public PassCertificateController(IPassCertificateService certificateService)
            : base(certificateService)
        {
        }

        public override ActionResult Create()
        {
            ViewBag.HtmlFormAttributes = new {enctype = "multipart/form-data"};
            //TODO temp solution to specify ExpDate
            return View(new PassCertificateAppleViewModel() { ExpDate = DateTime.Today });
        }

        public override ActionResult Edit(int id)
        {
            ViewBag.HtmlFormAttributes = new { enctype = "multipart/form-data" };
            return base.Edit(id);
        }

        public override ActionResult Create(PassCertificateAppleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CertificateFile != null && model.CertificateFile.ContentLength > 0)
                {
                    PassCertificateApple passCert = Mapper.Map<PassCertificateAppleViewModel, PassCertificateApple>(model);
                    _service.UploadCertificate(passCert, model.Password, model.CertificateFile.InputStream);
                    passCert.CertificateFileName = model.CertificateFile.FileName;
                    _service.Create(passCert);
                    return RedirectTo(model);
                }

                ModelState.AddModelError(string.Empty, "Certificate file should be specified");
            }

            return View(model);
        }

        public override ActionResult Edit(PassCertificateAppleViewModel model)
        {
            if (ModelState.IsValid)
            {
                PassCertificateApple passCert = _service.Get(model.EntityId);
                passCert = Mapper.Map<PassCertificateAppleViewModel, PassCertificateApple>(model, passCert);

                if (model.CertificateFile != null && model.CertificateFile.ContentLength > 0)
                {
                    _service.UploadCertificate(passCert, model.Password, model.CertificateFile.InputStream);
                    passCert.CertificateFileName = model.CertificateFile.FileName;
                }

                _service.Update(passCert);
                return RedirectTo(model);
            }

            return View(model);
        }

        public FileResult Download(int id)
        {
            PassCertificateApple passCertificate = _service.Get(id);
            Stream file = _service.DownloadCertificate(passCertificate.CertificateStorageId);
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, passCertificate.CertificateFileName ?? "Certificate_" + passCertificate.Name);
        }
    }
}