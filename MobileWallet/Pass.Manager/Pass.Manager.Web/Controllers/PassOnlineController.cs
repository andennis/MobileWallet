using System.IO;
using System.Web.Mvc;
using Common.BL;
using Common.Utils;
using Common.Web.Controls.Grid;
using Pass.Container.Core.SearchFilters;
using Pass.Container.Core.Entities;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Controllers
{
    [Authorize]
    public class PassOnlineController : BaseController
    {
        private readonly IPassOnlineService _passOnlineService;
        private readonly IPassNotificationService _notificationService;
        private readonly IPassContentService _passContentService;

        public PassOnlineController(IPassOnlineService passOnlineService, 
            IPassNotificationService notificationService,
            IPassContentService passContentService)
        {
            _passOnlineService = passOnlineService;
            _notificationService = notificationService;
            _passContentService = passContentService;
        }

        [AjaxOnly]
        public ActionResult Register(int id)
        {
            _passOnlineService.Register(id);
            PassContent pc = _passContentService.Get(id);
            var data = new
            {
                pc.ContainerPassId,
                pc.SerialNumber,
                pc.AuthToken
            };
            return JsonEx(data, true, Resources.Resources.RegisterPassOnlineSuccess);
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

        [AjaxOnly]
        public ActionResult Registrations(GridDataRequest request, PassRegistrationFilter searchFilter)
        {
            SearchResult<RegistrationInfo> result = _passOnlineService.GetPassRegistrations(GridRequestToSearchContext(request), searchFilter);
            return Json(GridDataResponse.Create(request, result.Data, result.TotalCount), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult NotifyClientDevice(int passContentId, int clientDeviceId)
        {
            _notificationService.NotifyClientDevice(passContentId, clientDeviceId);
            return JsonEx(true, Resources.Resources.PushClientDeviceSuccess);
        }

        [AjaxOnly]
        public ActionResult NotifyClientDevices(int passContentId)
        {
            _notificationService.NotifyClientDevices(passContentId);
            return JsonEx(true, Resources.Resources.PushClientDevicesSuccess);
        }

    }
}