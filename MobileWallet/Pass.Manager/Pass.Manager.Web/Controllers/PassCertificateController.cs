using System.IO;
using System.Web.Mvc;
using AutoMapper;
using Common.BL;
using Common.Utils;
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
            SetFormAttributes(new {enctype = "multipart/form-data"});
            return base.Create();
        }

        public override ActionResult Edit(int id)
        {
            SetFormAttributes(new { enctype = "multipart/form-data" });
            return base.Edit(id);
        }

        public override ActionResult Create(PassCertificateAppleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CertificateFile != null && model.CertificateFile.ContentLength > 0)
                {
                    PassCertificateApple passCert = Mapper.Map<PassCertificateAppleViewModel, PassCertificateApple>(model);
                    var fileInfo = new FileContentInfo()
                    {
                        FileName = model.CertificateFile.FileName,
                        ContentStream = model.CertificateFile.InputStream,
                        ContentType = model.CertificateFile.ContentType
                    };
                    _service.UploadCertificate(passCert, model.Password, fileInfo);
                    passCert.CertificateFileName = model.CertificateFile.FileName;
                    _service.Create(passCert);
                    return RedirectTo(model);
                }

                ModelState.AddModelError(string.Empty, Resources.Resources.CertificateFileNotSpecified);
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
                    var fileInfo = new FileContentInfo()
                                   {
                                       FileName = model.CertificateFile.FileName, 
                                       ContentStream = model.CertificateFile.InputStream,
                                       ContentType = model.CertificateFile.ContentType
                                   };
                    _service.UploadCertificate(passCert, model.Password, fileInfo);
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