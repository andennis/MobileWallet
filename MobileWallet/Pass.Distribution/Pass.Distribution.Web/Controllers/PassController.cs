using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Utils;
using Pass.Container.Core.Entities.Enums;
using Pass.Distribution.Core.Entities;
using Pass.Distribution.Core.Exceptions;
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
        [ActionName("e-t")]
        public ActionResult Edit(string token)
        {
            ClientType clientType = GetClientType();
            if (clientType == ClientType.Unknown)
                return RedirectToAction("NotSupported");

            PassTokenInfo pti = _distribService.DecryptPassToken(token);
            if (!pti.PassContentId.HasValue)
                throw new PassDistributionGeneralException("Token does not contain PassContentId");

            IEnumerable<DistribField> passFields = _distribService.GetPassFields(pti.PassContentId.Value);
            var model = new PassDistributionModel()
            {
                PassToken = token,
                PassFields = passFields.ToArray()
            };
            return View("Edit", model);
        }

        [HttpGet]
        [ActionName("e-id")]
        public ActionResult Edit(int id)
        {
            string token = _distribService.EncryptPassToken(new PassTokenInfo() {PassContentId = id});
            return Edit(token);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PassDistributionModel model)
        {
            PassTokenInfo pti = _distribService.DecryptPassToken(model.PassToken);
            if (!pti.PassContentId.HasValue)
                throw new PassDistributionGeneralException("Token does not contain PassContentId");

            _distribService.UpdatePassFields(pti.PassContentId.Value, model.PassFields);
            return RedirectToAction("Download", new { token = model.PassToken });
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

            PassTokenInfo pti = _distribService.DecryptPassToken(token);
            if (!pti.PassContentId.HasValue)
                throw new PassDistributionGeneralException("Token does not contain PassContentId");

            FileContentInfo fileInfo = _distribService.GetPassPackage(pti.PassContentId.Value, clientType);
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