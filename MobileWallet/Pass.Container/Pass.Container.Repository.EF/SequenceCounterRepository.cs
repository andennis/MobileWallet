using System;
using System.Data;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using Common.Repository.EF;
using System.Data.SqlClient;

namespace Pass.Container.Repository.EF
{
    public class SequenceCounterRepository : Repository<SequenceCounter>, ISequenceCounterRepository
    {
        public SequenceCounterRepository(DbContextBase dbContext)
            : base(dbContext)
        {
            
        }

        public int GetNextSerialNumber(string counterName)
        {
            var prmNaxtValue = new SqlParameter("NextValue", SqlDbType.Int) {Direction = ParameterDirection.Output};
            ExecuteNonQueryStoredProc(DbScheme + ".SequenceCounter_NextValue",
                                          new SqlParameter("Name", counterName),
                                          prmNaxtValue);

            return Convert.ToInt32(prmNaxtValue.Value);
        }
    }
}
