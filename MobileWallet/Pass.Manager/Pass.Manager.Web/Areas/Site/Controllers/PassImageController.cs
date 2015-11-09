using System.Web.Mvc;
using Common.Utils;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Controllers
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
            FileContentInfo fci = _service.GetImage(id);
            if (fci == null)
                return Content(string.Empty);

            return File(fci.ContentStream, fci.ContentType);
        }
    }
}