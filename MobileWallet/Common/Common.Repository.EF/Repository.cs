using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Repository.EF
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        protected readonly DbContextBase _dbContext;

        public Repository(DbContextBase dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        protected string DbScheme { get { return _dbContext.DbScheme; } }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IQueryable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            UpdateNulltoDBNull(parameters);
            return _dbSet.SqlQuery(query, parameters).AsQueryable();
        }
        public virtual IQueryable<T> SqlQuery<T>(string query, params object[] parameters)
        {
            UpdateNulltoDBNull(parameters);
            return _dbContext.Database.SqlQuery<T>(query, parameters).AsQueryable();
        }
        public virtual T SqlQueryScalar<T>(string query, params object[] parameters)
        {
            UpdateNulltoDBNull(parameters);
             return _dbContext.Database.SqlQuery<T>(query, parameters).FirstOrDefault();
        }

        public virtual IQueryable<TEntity> SqlQueryStoredProc(string spName, params object[] parameters)
        {
            string commandText = GetSqlCommandText(spName, parameters);
            return SqlQuery(commandText.TrimEnd(), parameters);
        }
        public virtual IQueryable<T> SqlQueryStoredProc<T>(string spName, params object[] parameters)
        {
            string commandText = GetSqlCommandText(spName, parameters);
            return SqlQuery<T>(commandText.TrimEnd(), parameters);
        }
        public virtual T SqlQueryScalarStoredProc<T>(string spName, params object[] parameters)
        {
            string commandText = GetSqlCommandText(spName, parameters);
            return SqlQueryScalar<T>(commandText.TrimEnd(), parameters);
        }

        public void ExecuteCommand(string commandText, params object[] parameters)
        {
            UpdateNulltoDBNull(parameters);
            _dbContext.Database.ExecuteSqlCommand(commandText, parameters);
        }

        public void ExecuteNonQueryStoredProc(string spName, params object[] parameters)
        {
            string commandText = GetSqlCommandText(spName, parameters);
            ExecuteCommand(commandText.TrimEnd(), parameters);
        }

        private void UpdateNulltoDBNull(IEnumerable<object> parameters)
        {
            foreach (IDbDataParameter prm in parameters.Cast<IDbDataParameter>())
            {
                if (prm.Value == null)
                    prm.Value = DBNull.Value;
            }
        }

        protected string GetSqlCommandText(string spName, params object[] parameters)
        {
            if (parameters == null || !parameters.Any())
                return spName;

            string prmNames = string.Join(",", parameters.Cast<IDbDataParameter>().Select(x => string.Format("@{0}=@{0}" + (x.Direction == ParameterDirection.Output ? " OUTPUT" : string.Empty), x.ParameterName)));
            return spName + (prmNames!=string.Empty ? " " + prmNames : string.Empty);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
        }
        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }
        public virtual void Delete(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbSet.Remove(entity);
        }

        public virtual IRepositoryQuery<TEntity> Query()
        {
            return new RepositoryQuery<TEntity>(this);
        }

        internal IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => query = query.Include(i));

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
            {
                query = query
                    .Skip((page.Value - 1)*pageSize.Value)
                    .Take(pageSize.Value);
            }
            return query;
        }

        /// <summary>
        /// It executes the SP with name pattern: 
        ///     [Db scheme].[TEntity type name].GetView
        /// The SP should take the parameter: @ID INT
        /// </summary>
        /// <typeparam name="TEntityView"></typeparam>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public virtual TEntityView GetView<TEntityView>(int entityId) where TEntityView : class
        {
            string spName = string.Format("{0}.{1}_GetView", DbScheme, typeof(TEntity).Name);
            return SqlQueryStoredProc<TEntityView>(spName, new SqlParameter("ID", entityId)).FirstOrDefault();
        }

        /// <summary>
        /// It executes the SP with name pattern: 
        ///     [Db scheme].[TEntity type name].Search
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
        public virtual IEnumerable<TEntityView> Search<TEntityView>(IEnumerable<QueryParameter> searchParams, out int totalRecords)
        {
            IList<object> sqlPrms = searchParams != null
                ? searchParams.Select(x => (object)new SqlParameter(x.Name, x.Value)).ToList()
                : null;

            var prm = new SqlParameter("TotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };
            if (sqlPrms != null)
                sqlPrms.Add(prm);
            else
                sqlPrms = new List<object>() { prm };

            string spName = string.Format("{0}.{1}_Search", DbScheme, typeof(TEntity).Name);
            IEnumerable<TEntityView> result = SqlQueryStoredProc<TEntityView>(spName, sqlPrms.ToArray()).ToList();
            totalRecords = Convert.ToInt32(prm.Value);
            return result;
        }

    }
}