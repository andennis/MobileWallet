using System.Collections.Generic;
using Common.Utils;
using Pass.Container.Core.Entities.Enums;
using Pass.Distribution.Core.Entities;

namespace Pass.Distribution.Core.Services
{
    public interface IPassDistributionService
    {
        string GetPassToken(int passContentId);
        //string GetPassTemplateToken(int passContentTemplateId);
        IEnumerable<DistribField> GetPassFields(int passContentId);
        void UpdatePassFields(int passContentId, IEnumerable<DistribField> passFields);
        FileContentInfo GetPassPackage(string token, ClientType clientType);
    }
}