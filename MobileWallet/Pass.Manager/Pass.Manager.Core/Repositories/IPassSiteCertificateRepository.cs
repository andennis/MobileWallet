using System.Collections.Generic;
using Common.Repository;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core.Repositories
{
    public interface IPassSiteCertificateRepository : IRepository<PassSiteCertificate>
    {
        IEnumerable<PassCertificate> GetUnassignedCertificates(int passSiteId);
    }
}
