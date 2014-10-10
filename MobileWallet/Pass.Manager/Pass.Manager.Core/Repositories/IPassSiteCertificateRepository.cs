using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core.Repositories
{
    public interface IPassSiteCertificateRepository : IRepository<PassSiteCertificate>
    {
        IEnumerable<PassCertificate> GetPassCertificates(int passSiteId);
    }
}
