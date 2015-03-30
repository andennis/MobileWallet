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
        public PassImageController(IPassImageService imageService)
            : base(imageService)
        {
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
            return base.Create(model);
        }

        public ActionResult GetImage(int id)
        {
            return File("", "");
        }
    }
}