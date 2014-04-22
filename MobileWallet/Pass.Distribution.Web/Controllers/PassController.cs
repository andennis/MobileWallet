using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Common.Extensions;
using Common.Utils;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Exceptions;
using Pass.Distribution.Web.Models;

namespace Pass.Distribution.Web.Controllers
{
    public class PassController : Controller
    {
        private const string SequrityKey = "1234567890987654";

        private readonly IPassContainerService _passContainerService;
        private readonly IPassTemplateService _passTemplateService;

        public PassController(IPassContainerService passContainerService, IPassTemplateService passTemplateService)
        {
            _passContainerService = passContainerService;
            _passTemplateService = passTemplateService;
        }

        public ActionResult EditPass(int id)
        {
            ClientType clientType = GetClientType();
            if (clientType == ClientType.Unknown)
                return RedirectToAction("NotSupported");

            IList<PassFieldInfo> passFields = _passContainerService.GetPassFields(id);

            var tokenInfo = new PassTokenInfo(){Id = id};
            var model = new PassModel()
            {
                PassToken = EncryptPassToken(tokenInfo),
                PassFields = passFields
            };

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult EditPass(PassModel model)
        {
            PassTokenInfo tokenInfo = DecryptPassToken(model.PassToken);
            _passContainerService.UpdatePassFields(tokenInfo.Id, model.PassFields);
            return RedirectToAction("Download", new { token = model.PassToken });
        }

        public ActionResult CreatePass(int id)
        {
            ClientType clientType = GetClientType();
            if (clientType == ClientType.Unknown)
                return RedirectToAction("NotSupported");

            IList<PassFieldInfo> passFields = _passTemplateService.GetPassTemplateFields(id);

            var tokenInfo = new PassTokenInfo() { Id = id };
            var model = new PassModel()
            {
                PassToken = EncryptPassToken(tokenInfo),
                PassFields = passFields
            };

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult CreatePass(PassModel model)
        {
            PassTokenInfo tokenInfo = DecryptPassToken(model.PassToken);
            tokenInfo.Id = _passContainerService.CreatePass(tokenInfo.Id, model.PassFields);
            string token = EncryptPassToken(tokenInfo);
            return RedirectToAction("Download", new { token });
        }

        public ActionResult Download(string token)
        {
            ClientType clientType = GetClientType();
            if (clientType == ClientType.Unknown)
                return RedirectToAction("NotSupported");

            PassTokenInfo tokenInfo = DecryptPassToken(token);
            string path = _passContainerService.GetPassPackage(tokenInfo.Id, clientType);
            return File(path, "application/vnd.apple.pkpass", Path.GetFileName(path));
        }

        public ActionResult NotSupported()
        {
            NotSupportedModel model = GetNotSupportedModel();
            return View(model);
        }

        private string EncryptPassToken(PassTokenInfo tokenInfo)
        {
            return PassDistribution.EncryptPassToken(tokenInfo, SequrityKey);
        }
        private PassTokenInfo DecryptPassToken(string passToken)
        {
            return PassDistribution.DecryptPassToken(passToken, SequrityKey);
        }

        /*
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
            string path = HttpContext.Server.MapPath("~/App_Data/1.pkpass");
            return File(path, "application/vnd.apple.pkpass", "1.pkpass");
        }

        private void SendLinkToEmail(string email)
        {
            
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