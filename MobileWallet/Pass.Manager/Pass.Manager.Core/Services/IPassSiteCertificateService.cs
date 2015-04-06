using System.Collections.Generic;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassSiteCertificateService : IPassManagerServiceBase<PassSiteCertificate, PassSiteCertificateFilter>
    {
        IEnumerable<PassCertificate> GetUnassignedCertificates(int passSiteId);
        IEnumerable<PassCertificate> GetCertificates(int passSiteId);
    }
}
