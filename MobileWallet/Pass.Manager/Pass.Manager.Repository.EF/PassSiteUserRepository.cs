using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Common.Repository.EF;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Repositories;

namespace Pass.Manager.Repository.EF
{
    public class PassSiteUserRepository : PassManagerRepository<PassSiteUser>, IPassSiteUserRepository
    {
        public PassSiteUserRepository(DbContextBase dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<User> GetUnassignedUsers(int passSiteId)
        {
            return SqlQuery<User>(DbScheme + ".PassSiteUser_GetUnassignedUsers @PassSiteId",
                                          new SqlParameter("PassSiteId", passSiteId)).ToList();
        }

    }
}
