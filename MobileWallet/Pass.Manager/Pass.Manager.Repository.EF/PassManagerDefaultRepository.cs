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

        /// <summary>
        /// It executes the SP with name pattern: 
        ///     [TEntity type name].Search
        /// 
        /// The minimal set of parameters:
        /// ------------------------------
        ///     @PageIndex INT,
        ///     @PageSize INT,
        ///     @SortBy VARCHAR(64),
        ///     @SortDirection INT,
        ///     @TotalRecords INT OUTPUT,
        ///     @SearchText NVARCHAR(MAX)
        /// ------------------------------
        /// </summary>
        /// <typeparam name="TEntityView"></typeparam>
        /// <param name="searchParams"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntityView> Search<TEntityView>(IDictionary<string, object> searchParams, out int totalRecords)
        {
            IList<object> sqlPrms = searchParams != null
                ? searchParams.Select(x => (object)new SqlParameter(x.Key, x.Value)).ToList()
                : null;

            var prm = new SqlParameter("TotalRecords", SqlDbType.Int) {Direction = ParameterDirection.Output};
            if (sqlPrms != null)
                sqlPrms.Add(prm);
            else
                sqlPrms = new List<object>() { prm };

            string paramNames = string.Join(",", sqlPrms.Cast<SqlParameter>().Select(x => string.Format("@{0}=@{0}" + (x.Direction == ParameterDirection.Output ? " OUTPUT" : string.Empty), x.ParameterName)));
            string query = string.Format("{0}.{1}_Search {2}", DbScheme, typeof (TEntity).Name, paramNames);
            IEnumerable<TEntityView> result = SqlQuery<TEntityView>(query, sqlPrms.ToArray()).ToList();
            totalRecords = Convert.ToInt32(prm.Value);
            return result;
        }
    }
}
