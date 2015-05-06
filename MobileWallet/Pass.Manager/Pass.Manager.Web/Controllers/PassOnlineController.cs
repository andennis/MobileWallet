using System.IO;
using System.Web.Mvc;
using Common.Utils;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Controllers
{
    [Authorize]
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
            string fileName = string.Format("pass{0}{1}", id, Path.GetExtension(fileInfo.FileName));
            return File(fileInfo.ContentStream, fileInfo.ContentType, fileName);
        }

    }
}