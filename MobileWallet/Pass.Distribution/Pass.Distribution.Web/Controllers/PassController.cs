using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Utils;
using Pass.Container.Core.Entities.Enums;
using Pass.Distribution.Core.Entities;
using Pass.Distribution.Core.Services;
using Pass.Distribution.Web.Models;

namespace Pass.Distribution.Web.Controllers
{
    public class PassController : Controller
    {
        private readonly IPassDistributionService _distribService;

        public PassController(IPassDistributionService distribService)
        {
            _distribService = distribService;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ClientType clientType = GetClientType();
            if (clientType == ClientType.Unknown)
                return RedirectToAction("NotSupported");

            IEnumerable<DistribField> passFields = _distribService.GetPassFields(id);
            var model = new PassDistributionModel()
                        {
                            PassContentId = id,
                            PassFields = passFields.ToArray()
                        };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PassDistributionModel model)
        {
            _distribService.UpdatePassFields(model.PassContentId, model.PassFields);
            string token = _distribService.GetPassToken(model.PassContentId);
            return RedirectToAction("Download", new { token });
        }

        /*
        [HttpGet]
        public ActionResult Create(int id)
        {
            ClientType clientType = GetClientType();
            if (clientType == ClientType.Unknown)
                return RedirectToAction("NotSupported");

            IList<PassFieldInfo> passFields = _passTemplateService.GetPassTemplateFields(id);

            var tokenInfo = new PassTokenInfo() { Id = id };
            var model = new PassDistributionModel()
            {
                PassToken = EncryptPassToken(tokenInfo),
                PassFields = passFields
            };

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Create(PassDistributionModel model)
        {
            PassTokenInfo tokenInfo = DecryptPassToken(model.PassToken);
            tokenInfo.Id = _passService.CreatePass(tokenInfo.Id, model.PassFields);
            string token = EncryptPassToken(tokenInfo);
            return RedirectToAction("Download", new { token });
        }
        */

        public ActionResult Download(string token)
        {
            ClientType clientType = GetClientType();
            if (clientType == ClientType.Unknown)
                return RedirectToAction("NotSupported");

            FileContentInfo fileInfo = _distribService.GetPassPackage(token, clientType);
            return File(fileInfo.ContentStream, "application/vnd.apple.pkpass", fileInfo.FileName);
        }

        public ActionResult NotSupported()
        {
            NotSupportedModel model = GetNotSupportedModel();
            return View(model);
        }

        private ClientType GetClientType()
        {
            if (Request.Browser.IsMobileDevice)
            {
                if (Request.Browser.MobileDeviceManufacturer == "Apple" && Request.Browser.MobileDeviceModel == "IPhone")
                    return ClientType.Apple;

                return ClientType.Unknown;
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