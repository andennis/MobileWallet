using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Utils;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Controllers
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
            _templateOnlineService.Register(id);
            return JsonEx();
        }

        [AjaxOnly]
        public ActionResult Unregister(int id)
        {
            throw new NotImplementedException();
        }

        [AjaxOnly]
        public ActionResult Update(int id)
        {
            _templateOnlineService.UpdateOnlineTemplete(id);
            return JsonEx();
        }

        public ActionResult Download(int id)
        {
            FileContentInfo fileInfo = _templateOnlineService.GetTemplateArchive(id);
            return File(fileInfo.ContentStream, fileInfo.ContentType, fileInfo.FileName);
        }

    }
}