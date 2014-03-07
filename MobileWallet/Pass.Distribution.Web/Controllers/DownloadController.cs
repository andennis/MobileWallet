using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Distribution.Web.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IPassDistributionService _passDistService;

        public DownloadController(IPassDistributionService passDistService)
        {
            _passDistService = passDistService;
        }

        //
        // GET: /PassDistribution/
        public FileResult Download(string id)
        {
            //TODO temp code just for test
            string path = HttpContext.Server.MapPath("~/App_Data/TextFile1.txt");
            return File(path, "text/plain");
        }

        public FileResult DownloadPass(string passToken)
        {
            //PassTokenInfo ptInfo = _passDistService.GetPassTokenInfo(passToken);
            //DeviceType clientType = GetClientType();
            //_passDistService.GetPassPackage();
            string path = HttpContext.Server.MapPath("~/App_Data/Test1.pkpass");
            return File(path, "application/vnd.apple.pkpass");
        }

        public ContentResult ClientType()
        {
            var v = Request.Browser;
            return Content(Request.Browser.Browser);
        }

        private DeviceType GetClientType()
        {
            if (Request.Browser.IsMobileDevice)
            {
                if (Request.Browser.MobileDeviceManufacturer == "Apple" && Request.Browser.MobileDeviceModel == "IPhone")
                    return DeviceType.Apple;
            }
            return DeviceType.Browser;
        }
	}
}