using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Exceptions;
using Pass.Distribution.Web.Models;

namespace Pass.Distribution.Web.Controllers
{
    public class PassController : Controller
    {
        private readonly IPassDistributionService _passDistService;

        public PassController(IPassDistributionService passDistService)
        {
            _passDistService = passDistService;
        }

        public ActionResult Index(string token)
        {
            //PassTokenInfo tokenInfo = _passDistService.DecryptPassToken(token);
            var model = new PassModel()
                            {
                                PassToken = token,
                                PassFields = new List<PassFieldInfo>(){new PassFieldInfo(){Label = "L1", Value = "V1"}}
                            };
            return View(model);
        }
        public FileResult Download(string id)
        {
            //TODO temp code just for test
            string path = HttpContext.Server.MapPath("~/App_Data/TextFile1.txt");
            return File(path, "text/plain");
        }

        public FileResult DownloadPass(string passToken)
        {
            //Stream pkgStream = GetPassPackage(passToken);
            string path = HttpContext.Server.MapPath("~/App_Data/Test1.pkpass");
            return File(path, "application/vnd.apple.pkpass");
        }

        /*
        private Stream GetPassPackage(string passToken)
        {
            PassTokenInfo ptInfo = _passDistService.DecryptPassToken(passToken);
            if (!ptInfo.PassTemplateId.HasValue && !ptInfo.PassId.HasValue)
                throw new PassDistributionException("Pass token is not valid: " + passToken);

            ClientType clientType = GetClientType();

            return ptInfo.PassTemplateId.HasValue 
                ? _passDistService.GetPassPackageByTemplate(ptInfo.PassTemplateId.Value, clientType)
                : _passDistService.GetPassPackage(ptInfo.PassId.Value, clientType);
        }
        */

        private ClientType GetClientType()
        {
            if (Request.Browser.IsMobileDevice)
            {
                if (Request.Browser.MobileDeviceManufacturer == "Apple" && Request.Browser.MobileDeviceModel == "IPhone")
                    return ClientType.Apple;

                throw new PassDistributionException(string.Format("The client '{0}' - '{1}' is not supported", 
                    Request.Browser.MobileDeviceManufacturer, Request.Browser.MobileDeviceModel));
            }
            return ClientType.Browser;
        }
	}
}