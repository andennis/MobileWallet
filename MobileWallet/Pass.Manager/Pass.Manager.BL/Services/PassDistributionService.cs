using System;
using System.IO;
using Common.Extensions;
using Common.Utils;
using Newtonsoft.Json;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Exceptions;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassDistributionService : IPassDistributionService
    {
        private const string SequrityKey = "1234567890987654";
        private const string SecurityVector = "nh2!0hg#vbPu&QSd";

        private readonly IPassService _passService;
        private readonly IPassContentService _passContentService;

        private class PassTokenInfo
        {
            [JsonProperty(PropertyName = "p")]
            public int? PassContentId { get; set; }

            [JsonProperty(PropertyName = "t")]
            public int? PassContentTemplateId { get; set; }
        }

        public PassDistributionService(IPassContentService passContentService, IPassService passService)
        {
            _passService = passService;
            _passContentService = passContentService;
            
        }
        public string GetPassToken(int passContentId)
        {
            string token = string.Format("{0}:{1}", "p", passContentId);
            return Crypto.EncryptString(token, SequrityKey, SecurityVector);
        }

        public string GetPassTemplateToken(int passContentTemplateId)
        {
            string token = string.Format("{0}:{1}", "t", passContentTemplateId);
            return Crypto.EncryptString(token, SequrityKey, SecurityVector);
        }

        public FileContentInfo GetPassPackage(string token)
        {
            token = Crypto.DecryptString(token, SequrityKey, SecurityVector);
            token = string.Format("{{{0}}}", token);
            var tokenInfo = token.JsonToObject<PassTokenInfo>();
            if (tokenInfo.PassContentId.HasValue)
            {
                PassContent pc = _passContentService.GetDetails(tokenInfo.PassContentId.Value);
                if (!pc.ContainerPassId.HasValue)
                    throw new PassManagerGeneralException(string.Format("PassContentId: {0} has not been registered yet", tokenInfo.PassContentId));

                string packagePath = _passService.GetPassPackage(pc.ContainerPassId.Value, ClientType.Apple);

                var fs = new FileStream(packagePath, FileMode.Open, FileAccess.Read);
                return new FileContentInfo()
                {
                    ContentStream = fs,
                    FileName = Path.GetFileName(packagePath),
                    ContentType = "application/vnd.apple.pkpass"
                };
            }

            if (tokenInfo.PassContentTemplateId.HasValue)
            {
                throw new NotImplementedException();
            }

            return null;
        }
    }
}
