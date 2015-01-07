using System.Collections.Generic;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassSiteCertificateService : IBaseService<PassSiteCertificate, PassSiteCertificateFilter>
    {
        IEnumerable<PassCertificate> GetUnassignedCertificates(int passSiteId);
    }
}
