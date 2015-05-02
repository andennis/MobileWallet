using System.Web.Mvc;
using Common.Utils;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Controllers
{
    public class PassOnlineController : BaseController
    {
        private readonly IPassOnlineService _passOnlineService;

        public PassOnlineController(IPassOnlineService passOnlineService)
        {
            _passOnlineService = passOnlineService;
        }

        [AjaxOnly]
        public ActionResult Register(int id)
        {
            int containerPassId = _passOnlineService.Register(id);
            return JsonEx(new {containerPassId}, true, Resources.Resources.RegisterPassOnlineSuccess);
        }

        [AjaxOnly]
        public ActionResult Update(int id)
        {
            _passOnlineService.UpdateOnline(id);
            return JsonEx(true, Resources.Resources.UpdatePassOnlineSuccess);
        }

        public ActionResult Download(int id)
        {
            FileContentInfo fileInfo = _passOnlineService.GetPassPackage(id);
            return File(fileInfo.ContentStream, fileInfo.ContentType, fileInfo.FileName);
        }

    }
}