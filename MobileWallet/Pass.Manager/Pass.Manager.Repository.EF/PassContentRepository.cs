﻿using Common.Repository.EF;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF
{
    public class PassContentRepository : Repository<PassContent>
    {
        public PassContentRepository(DbContextBase dbContext)
            : base(dbContext)
        {
        }

        /*
        public override void Insert(PassContent entity)
        {
            var prmPassContentId = new SqlParameter("PassContentId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            //TODO the SP should be run in transaction
            ExecuteNonQueryStoredProc(DbScheme + ".PassContent_Insert",
                prmPassContentId,
                new SqlParameter("SerialNumber", entity.SerialNumber),
                new SqlParameter("AuthToken", entity.AuthToken),
                new SqlParameter("ExpDate", entity.ExpDate),
                new SqlParameter("PassContentTemplateId", entity.PassContentTemplateId));

            entity.ContainerPassId = Convert.ToInt32(prmPassContentId.Value);
        }
        */
    }
}
