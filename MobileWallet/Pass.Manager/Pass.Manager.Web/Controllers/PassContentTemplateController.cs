using System.Web.Mvc;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassContentTemplateController : BaseEntityController<PassContentTemplateViewModel, PassContentTemplate, IPassContentTemplateService, PassContentTemplateFilter>
    {
        public PassContentTemplateController(IPassContentTemplateService templateService)
            : base(templateService)
        {
        }

        [HttpGet]
        public ActionResult CreateContent(int passProjectId)
        {
            return Create(m => m.PassProjectId = passProjectId);
        }

    }
}