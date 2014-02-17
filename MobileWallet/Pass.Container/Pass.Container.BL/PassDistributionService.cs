using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utils;
using Pass.Container.Core;

namespace Pass.Container.BL
{
    public class PassDistributionService : IPassDistributionService
    {
        private const string SecurityVector = "nh2!0hg#vbPu&QSd";
        private readonly IPassDistributionConfig _config;

        public PassDistributionService(IPassDistributionConfig config)
        {
            _config = config;
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
            string tokenInfo = string.Format("PS_ID={0}", passId);
            return Crypto.EncryptString(tokenInfo, _config.SecurityKey, SecurityVector);
        }

        public string GetPassTemplateToken(int passTempleteId)
        {
            string tokenInfo = string.Format("PST_ID={0}", passTempleteId);
            return Crypto.EncryptString(tokenInfo, _config.SecurityKey, SecurityVector);
        }
    }
}
