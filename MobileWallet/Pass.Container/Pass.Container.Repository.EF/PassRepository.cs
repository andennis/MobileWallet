using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Common.Repository.EF;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF
{
    public class PassRepository : Repository<Core.Entities.Pass>, IPassRepository
    {
        public PassRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public IList<ChangedPass> GetChangedPassesApple(string deviceId, string passTypeId, DateTime? updateTag)
        {
            return SqlQuery<ChangedPass>(PassContainerDbContext.DbScheme + ".GetChangedPassesApple @DeviceId, @PassTypeId, @UpdateTag",
                                          new SqlParameter("DeviceId", deviceId),
                                          new SqlParameter("PassTypeId", passTypeId),
                                          new SqlParameter("UpdateTag", (object)updateTag ?? DBNull.Value)).ToList();
        }

    }

}
