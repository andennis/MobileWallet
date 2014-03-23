using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Repository;
using Common.Utils;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

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

        #region IPassDistributionService
        public string GetPassPackage(PassTokenInfo passToken, ClientType deviceType, IList<PassFieldInfo> passFields)
        {
            throw new NotImplementedException();
        }

        public IList<PassFieldInfo> GetPassDistributionFields(PassTokenInfo passToken)
        {
            //Pass ID is specified
            if (passToken.PassId.HasValue)
            {
                IRepository<PassFieldValue> repPassFieldVal = _pcUnitOfWork.GetRepository<PassFieldValue>();
                return repPassFieldVal.Query()
                    .Filter(x => x.PassId == passToken.PassId.Value)
                    .Include(x => x.PassField)
                    .Get()
                    .Select(x => new PassFieldInfo()
                                     {
                                         PassFieldId = x.PassFieldId,
                                         Name = x.PassField.Name,
                                         Label = string.IsNullOrEmpty(x.Label) ? x.PassField.DefaultLabel : x.Label,
                                         Value = string.IsNullOrEmpty(x.Value) ? x.PassField.DefaultValue : x.Value,
                                     })
                    .ToList();
            }

            //Pass template ID is specified
            IRepository<PassField> repPassField = _pcUnitOfWork.GetRepository<PassField>();
            return repPassField.Query().Filter(x => x.PassTemplateId == passToken.PassTemplateId).Get()
                .Select(x => new PassFieldInfo()
                            {
                                PassFieldId = x.PassFieldId,
                                Name = x.Name,
                                Label = x.DefaultLabel ?? x.Name,
                                Value = x.DefaultValue
                            })
                .ToList();
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
            _pcUnitOfWork.Dispose();
        }
        #endregion

    }
}
