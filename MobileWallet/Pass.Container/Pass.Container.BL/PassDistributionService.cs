using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Utils;
using Newtonsoft.Json;
using Pass.Container.Core;
using Pass.Container.Core.Entities;

namespace Pass.Container.BL
{
    public class PassDistributionService : IPassDistributionService
    {
        private const string SecurityVector = "nh2!0hg#vbPu&QSd";
        private readonly IPassDistributionConfig _config;
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;

        public PassDistributionService(IPassDistributionConfig config, IPassContainerUnitOfWork pcUnitOfWork)
        {
            _config = config;
            _pcUnitOfWork = pcUnitOfWork;
        }

        public void CreateClientPass(int passTemplateId, IList<Core.Entities.PassFieldValue> passFieldValues)
        {
            throw new NotImplementedException();
        }

        public void GetClientPassPackage(int passId, Core.Entities.Enums.DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public void GetPassPackage(int passTemplateId, Core.Entities.Enums.DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public void UpdatePassFields(int passId, IList<Core.Entities.PassFieldValue> passFieldValues)
        {
            throw new NotImplementedException();
        }


        public string GetPassToken(int passId)
        {
            var pti = new PassTokenInfo() {PassId = passId.ToString()};
            return EncryptString(pti.ObjectToJson());
        }

        public string GetPassTemplateToken(int passTempleteId)
        {
            var pti = new PassTokenInfo() { PassTemplateId = passTempleteId.ToString() };
            return EncryptString(pti.ObjectToJson());
        }

        public PassTokenInfo GetPassTokenInfo(string passToken)
        {
            string decrypted = DecryptString(passToken);
            return decrypted.JsonToObject<PassTokenInfo>();
        }

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
            _pcUnitOfWork.Dispose();
        }
        #endregion
    }
}
