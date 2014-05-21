using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Common.Repository.EF;
using Pass.Container.Repository.Core;

namespace Pass.Container.Repository.EF
{
    public class PassRepository : Repository<Core.Entities.Pass>, IPassRepository
    {
        public PassRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public IList<string> GetSerialNumbersOfChangedPassesApple(string deviceId, string passTypeId, DateTime? updateTag)
        {
            return SqlQuery<string>(PassContainerDbContext.DbScheme + ".GetSerialNumbersOfChangedPassesApple @DeviceId, @PassTypeId, @UpdateTag",
                                          new SqlParameter("DeviceId", deviceId),
                                          new SqlParameter("PassTypeId", passTypeId),
                                          new SqlParameter("UpdateTag", updateTag)).ToList();
        }
    }
}
