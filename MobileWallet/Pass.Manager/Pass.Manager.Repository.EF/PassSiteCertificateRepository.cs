using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository.EF;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Repositories;

namespace Pass.Manager.Repository.EF
{
    public class PassSiteCertificateRepository : Repository<PassSiteCertificate>, IPassSiteCertificateRepository
    {
        public PassSiteCertificateRepository(DbContextBase dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<PassCertificate> GetPassCertificates(int passSiteId)
        {
            return SqlQuery<PassCertificate>(DbScheme + ".PassSiteCertificate_GetPassCertificates @PassSiteId",
                                          new SqlParameter("PassSiteId", passSiteId)).ToList();
        }

    }
}
