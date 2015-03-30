using System.Data.SqlClient;
using Common.Repository.EF;
using System.Collections.Generic;
using System.Linq;

namespace Pass.Manager.Repository.EF
{
    public class PassManagerDefaultRepository<TEntity> : Repository<TEntity> where TEntity : class
    {
        public PassManagerDefaultRepository(DbContextBase dbContext)
            :base(dbContext)
        {
        }

        public virtual IEnumerable<TEntityView> Search<TEntityView>(IDictionary<string, object> searchParams)
        {
            object[] sqlPrms = searchParams != null 
                ? searchParams.Select(x => (object)new SqlParameter(x.Key, x.Value)).ToArray()
                : null;

            string query = string.Format("{0}.{1}_Search", DbScheme, typeof (TEntity).Name);
            return SqlQuery<TEntityView>(query, sqlPrms);
        }
    }
}
