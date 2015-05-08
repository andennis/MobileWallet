using System.Collections.Generic;
using Common.Utils;
using Pass.Container.Core.Entities.Enums;
using Pass.Distribution.Core.Entities;

namespace Pass.Distribution.Core.Services
{
    public interface IPassDistributionService
    {
        string EncryptPassToken(PassTokenInfo tokenInfo);
        PassTokenInfo DecryptPassToken(string passToken);

        IEnumerable<DistribField> GetPassFields(int passContentId);
        void UpdatePassFields(int passContentId, IEnumerable<DistribField> passFields);

        IEnumerable<DistribField> GetPassTemplateFields(int passContentTemplateId);
        int CreatePass(int passContentTemplateId, IEnumerable<DistribField> passFields);

        FileContentInfo GetPassPackage(int passContentId, ClientType clientType);
    }
}