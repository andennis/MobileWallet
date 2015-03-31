using System;
using System.Collections;
using System.Data;
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

        public virtual IEnumerable<TEntityView> Search<TEntityView>(IDictionary<string, object> searchParams, out int totalRecords)
        {
            object[] sqlPrms = searchParams != null
                ? searchParams.Select(x => (object)new SqlParameter(x.Key, x.Value)).ToArray()
                : null;

            /*
            var prm = new SqlParameter("TotalRecords", SqlDbType.Int) {Direction = ParameterDirection.Output};
            if (sqlPrms != null)
                sqlPrms.Add(prm);
            else
                sqlPrms = new List<object>() { prm };
            */

            //string paramNames = string.Join(",", sqlPrms.Cast<SqlParameter>().Select(x => "@" + x.ParameterName + (x.Direction == ParameterDirection.Output ? " OUTPUT" : string.Empty)));
            string paramNames = string.Join(",", sqlPrms.Cast<SqlParameter>().Select(x => "@" + x.ParameterName));
            string query = string.Format("{0}.{1}_Search {2}", DbScheme, typeof (TEntity).Name, paramNames);
            IEnumerable<TEntityView> result = SqlQuery<TEntityView>(query, sqlPrms).ToList();
            totalRecords = 0;//Convert.ToInt32(prm.Value);
            return result;
        }
    }
}
