using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Utils;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Exceptions;

namespace Pass.Container.BL
{
    public class PassDistributionService : IPassDistributionService
    {
        private const string SecurityVector = "nh2!0hg#vbPu&QSd";

        private readonly IPassDistributionConfig _config;
        private readonly IPassContainerService _passContainerService;
        //private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IPassTemplateService _passTemplateService;

        public PassDistributionService(IPassDistributionConfig config, 
            //IPassContainerUnitOfWork pcUnitOfWork, 
            IPassContainerService passContainerService,
            IPassTemplateService passTemplateService)
        {
            _config = config;
            //_pcUnitOfWork = pcUnitOfWork;
            _passContainerService = passContainerService;
            _passTemplateService = passTemplateService;
        }

        #region IPassDistributionService
        public string GetPassPackage(PassTokenInfo passToken, ClientType deviceType)
        {
            throw new NotImplementedException();
        }

        public IList<PassFieldInfo> GetPassFields(PassTokenInfo passToken)
        {
            if (passToken == null)
                throw new ArgumentNullException("passToken");

            if (passToken.PassId.HasValue)
                return _passContainerService.GetPassFields(passToken.PassId.Value);

            if (!passToken.PassTemplateId.HasValue)
                throw new PassDistributionException("PassTemplateId and PassId are undefined in token");

            return _passTemplateService.GetPassTemplateFields(passToken.PassTemplateId.Value);
        }

        public string GetPassToken(int passId)
        {
            var pti = new PassTokenInfo() {PassId = passId};
            return EncryptString(pti.ObjectToJson());
        }
        public string GetPassTemplateToken(int passTempleteId)
        {
            var pti = new PassTokenInfo() { PassTemplateId = passTempleteId };
            return EncryptString(pti.ObjectToJson());
        }

        public PassTokenInfo DecryptPassToken(string passToken)
        {
            string decrypted = DecryptString(passToken);
            return decrypted.JsonToObject<PassTokenInfo>();
        }
        #endregion

        private string EncryptString(string textToEncrypt)
        {
            return Crypto.EncryptString(textToEncrypt, _config.SecurityKey, SecurityVector);
        }
        private string DecryptString(string textToDecrypt)
        {
            return Crypto.DecryptString(textToDecrypt, _config.SecurityKey, SecurityVector);
        }

        #region IDisposable
        public void Dispose()
        {
            //_pcUnitOfWork.Dispose();
        }
        #endregion

    }
}
