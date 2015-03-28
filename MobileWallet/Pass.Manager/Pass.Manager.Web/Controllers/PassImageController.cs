using System.Web.Mvc;
using AutoMapper;
using FileStorage.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassImageController : BaseEntityController<PassImageViewModel, PassImage, IPassImageService, PassImageFilter>
    {
        private readonly IFileStorageService _fileStorageService;

        public PassImageController(IPassImageService imageService, IFileStorageService fileStorageService)
            : base(imageService)
        {
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public ActionResult CreateImage(int contentTemplateId)
        {
            SetFormAttributes(new { enctype = "multipart/form-data" });
            return Create(m => m.PassContentTemplateId = contentTemplateId);
        }

        [ActionName("CreateImage")]
        public override ActionResult Create(PassImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                    model.FileStorageId = _fileStorageService.Put(model.ImageFile.InputStream);
                if (model.ImageFile2x != null && model.ImageFile2x.ContentLength > 0)
                    model.FileStorage2xId = _fileStorageService.Put(model.ImageFile2x.InputStream);

                PassImage entity = Mapper.Map<PassImageViewModel, PassImage>(model);
                _service.Create(entity);

                if (Request.IsAjaxRequest())
                    return JsonEx();
    
                return RedirectTo(model);
            }

            return CreateView(model);
        }


    }
}