using System;
using System.Collections;
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
        private readonly IPassContainerService _passContainerService;

        public PassController(IPassDistributionService passDistService, IPassContainerService passContainerService)
        {
            _passDistService = passDistService;
            _passContainerService = passContainerService;
        }

        public ActionResult Index(string token)
        {
            ClientType clientType = GetClientType();
            if (clientType == ClientType.Unknown)
                return RedirectToAction("NotSupported");

            //PassTokenInfo tokenInfo = _passDistService.DecryptPassToken(token);
            var passToken = new PassTokenInfo() {PassTemplateId = Convert.ToInt32(token)};
            IList<PassFieldInfo> passFields = _passDistService.GetPassFields(passToken);

            var model = new PassModel()
            {
                PassToken = token,
                PassFields = passFields
            };

            return View(model);
        }

        public ActionResult Test(string token)
        {
            ClientType clientType = GetClientType();
            if (clientType == ClientType.Unknown)
                return RedirectToAction("NotSupported");

            var model = new PassModel()
                            {
                                PassToken = token,
                                PassFields = new List<PassFieldInfo>()
                                                 {
                                                     new PassFieldInfo(){Label = "Label1", Value = "V1", Name = "F1"},
                                                     new PassFieldInfo(){Label = "Label2", Value = "V2", Name = "F2"}
                                                 }
                            };
            return View("Index", model);
        }

        public ActionResult NotSupported()
        {
            NotSupportedModel model = GetNotSupportedModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(PassModel model)
        {
            SendLinkToEmail(model.Email);

            int passTempleteId = Convert.ToInt32(model.PassToken);
            int passId = _passContainerService.CreatePass(passTempleteId, model.PassFields);

            string token = _passDistService.GetPassToken(passId);
            return RedirectToAction("Download", new { token });
        }
        
        public FileResult Download(string token)
        {
            //Stream pkgStream = GetPassPackage(passToken);
            //string path = HttpContext.Server.MapPath("~/../FS/4.pkpass");
            string path = HttpContext.Server.MapPath("~/App_Data/4.pkpass");
            return File(path, "application/vnd.apple.pkpass", "4.pkpass");
        }

        private void SendLinkToEmail(string email)
        {
            
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

                return ClientType.Unknown;
                /*
                throw new PassDistributionException(string.Format("The client '{0}' - '{1}' is not supported", 
                    Request.Browser.MobileDeviceManufacturer, Request.Browser.MobileDeviceModel));
                */
            }
            return ClientType.Browser;
        }
        private NotSupportedModel GetNotSupportedModel()
        {
            if (Request.Browser.IsMobileDevice)
                return new NotSupportedModel()
                            {
                                ClientManufacturer = Request.Browser.MobileDeviceManufacturer,
                                ClientModel = Request.Browser.MobileDeviceModel
                            };

            return new NotSupportedModel()
                        {
                            ClientManufacturer = Request.Browser.Browser,
                            ClientModel = Request.Browser.Type
                        };
        }
	}
}