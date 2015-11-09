using System;
using System.Web.Mvc;
using Common.Utils;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Controllers
{
    public class PassTemplateOnlineController : BaseController
    {
        private readonly IPassTemplateOnlineService _templateOnlineService;

        public PassTemplateOnlineController(IPassTemplateOnlineService templateOnlineService)
        {
            _templateOnlineService = templateOnlineService;
        }

        [AjaxOnly]
        public ActionResult Register(int id)
        {
            var containerTemplateId = _templateOnlineService.Register(id);
            return JsonEx(new { containerTemplateId }, true, Resources.Resources.RegisterTemplateOnlineSuccess);
        }

        [AjaxOnly]
        public ActionResult Unregister(int id)
        {
            throw new NotImplementedException();
        }

        [AjaxOnly]
        public ActionResult Update(int id)
        {
            _templateOnlineService.UpdateOnline(id);
            return JsonEx(true, Resources.Resources.UpdateTemplateOnlineSuccess);
        }

        public ActionResult Download(int id)
        {
            FileContentInfo fileInfo = _templateOnlineService.GetTemplateArchive(id);
            return File(fileInfo.ContentStream, fileInfo.ContentType, fileInfo.FileName);
        }

    }
}