using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Utils;
using Newtonsoft.Json;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;

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

        public int CreatePassByTemplate(int passTemplateId, IList<PassFieldValue> passFieldValues)
        {
            throw new NotImplementedException();
        }

        public Stream GetPassPackage(int passId, DeviceType deviceType)
        {
            throw new NotImplementedException();
        }
        public Stream GetPassPackageByTemplate(int passTemplateId, DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public void UpdatePassFields(int passId, IList<PassFieldValue> passFieldValues)
        {
            throw new NotImplementedException();
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
