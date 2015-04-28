using System.Data;
using System.Data.SqlClient;
using Common.Repository.EF;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF
{
    public class PassContentRepository : PassManagerDefaultRepository<PassContent>
    {
        public PassContentRepository(DbContextBase dbContext)
            : base(dbContext)
        {
        }

        public override void Insert(PassContent entity)
        {
            //TODO the SP should be run in transaction
            ExecuteNonQueryStoredProc(DbScheme + ".PassContent_Insert",
                new SqlParameter("PassContentId", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("SerialNumber", entity.SerialNumber),
                new SqlParameter("AuthToken", entity.AuthToken),
                new SqlParameter("ExpDate", entity.ExpDate),
                new SqlParameter("PassContentTemplateId", entity.PassContentTemplateId));
        }
    }
}
