using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Common.Repository.EF;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF
{
    public class PassRepository : Repository<Core.Entities.Pass>, IPassRepository
    {
        public PassRepository(DbContextBase dbContext)
            : base(dbContext)
        {
        }

        public IList<ChangedPass> GetChangedPassesApple(string deviceId, string passTypeId, DateTime? updateTag)
        {
            return SqlQueryStoredProc<ChangedPass>(DbScheme + ".GetChangedPassesApple",
                                          new SqlParameter("DeviceId", deviceId),
                                          new SqlParameter("PassTypeId", passTypeId),
                                          new SqlParameter("UpdateTag", updateTag)).ToList();
        }

        public Core.Entities.Pass GetPassApple(string serialNumber, string passTypeId)
        {
            return SqlQueryStoredProc<Core.Entities.Pass>(DbScheme + ".GetPassApple",
                                          new SqlParameter("SerialNumber", serialNumber),
                                          new SqlParameter("PassTypeId", passTypeId)).FirstOrDefault();
        }

    }

}
