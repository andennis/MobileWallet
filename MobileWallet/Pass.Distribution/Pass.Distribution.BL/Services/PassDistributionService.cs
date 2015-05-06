using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.BL;
using Common.Extensions;
using Common.Utils;
using Newtonsoft.Json;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Distribution.Core;
using Pass.Distribution.Core.Entities;
using Pass.Distribution.Core.Services;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Exceptions;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Distribution.BL.Services
{
    public class PassDistributionService : IPassDistributionService
    {
        private const string SecurityVector = "nh2!0hg#vbPu&QSd";

        //private readonly IPassService _passService;
        private readonly IPassContentService _passContentService;
        private readonly IPassContentFieldService _passContentFieldService;
        private readonly IPassDistributionConfig _config;
        private readonly IPassOnlineService _passOnlineService;

        private class PassTokenInfo
        {
            [JsonProperty(PropertyName = "p")]
            public int? PassContentId { get; set; }

            [JsonProperty(PropertyName = "t")]
            public int? PassContentTemplateId { get; set; }
        }

        public PassDistributionService(IPassDistributionConfig config, 
            IPassContentService passContentService,
            IPassContentFieldService passContentFieldService, 
            IPassOnlineService passOnlineService)
        {
            _config = config;
            //_passService = passService;
            _passContentService = passContentService;
            _passContentFieldService = passContentFieldService;
            _passOnlineService = passOnlineService;
        }

        public string GetPassToken(int passContentId)
        {
            return EncryptPassToken(new PassTokenInfo() { PassContentId = passContentId }, _config.SecurityKey);
        }

        /*
        public string GetPassTemplateToken(int passContentTemplateId)
        {
            return EncryptPassToken(new PassTokenInfo() { PassContentTemplateId = passContentTemplateId }, _config.SecurityKey);
        }
        */

        public IEnumerable<DistribField> GetPassFields(int passContentId)
        {
            SearchResult<PassContentFieldView> result = _passContentFieldService.SearchView<PassContentFieldView>(new SearchContext(), 
                new PassContentFieldFilter() { PassContentId = passContentId });

            return result.Data.Select(x => new DistribField()
                                           {
                                               DistribFieldId = x.PassContentFieldId, 
                                               Name = x.FieldName,
                                               Label = x.FieldLabel ?? x.FieldName,
                                               Value = x.FieldValue
                                           });
        }

        public void UpdatePassFields(int passContentId, IEnumerable<DistribField> passFields)
        {
            PassContent pc = _passContentService.GetDetails(passContentId);

            foreach (DistribField distribField in passFields)
            {
                PassContentField pcf = pc.Fields.FirstOrDefault(x => x.PassContentFieldId == distribField.DistribFieldId);
                if (pcf != null)
                    pcf.FieldValue = distribField.Value;
            }
            _passContentService.Update(pc);

            _passOnlineService.UpdateOnline(passContentId);
        }

        public FileContentInfo GetPassPackage(string token, ClientType clientType)
        {
            PassTokenInfo tokenInfo = DecryptPassToken(token, _config.SecurityKey);
            if (tokenInfo.PassContentId.HasValue)
                return _passOnlineService.GetPassPackage(tokenInfo.PassContentId.Value);

            if (tokenInfo.PassContentTemplateId.HasValue)
                throw new NotImplementedException();

            return null;
        }

        private static string EncryptPassToken(PassTokenInfo tokenInfo, string password)
        {
            string passToken = tokenInfo.ObjectToJson();
            return Crypto.EncryptString(passToken, password, SecurityVector);
        }
        private static PassTokenInfo DecryptPassToken(string passToken, string password)
        {
            string decrypted = Crypto.DecryptString(passToken, password, SecurityVector);
            return decrypted.JsonToObject<PassTokenInfo>();
        }

    }
}
