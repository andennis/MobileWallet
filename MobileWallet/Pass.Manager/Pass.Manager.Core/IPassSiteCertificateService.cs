using System.Collections.Generic;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core
{
    public interface IPassSiteCertificateService : IBaseService<PassSiteCertificate, PassSiteCertificateFilter>
    {
        IEnumerable<PassCertificate> GetPassCertificates(int passSiteId);
    }
}
