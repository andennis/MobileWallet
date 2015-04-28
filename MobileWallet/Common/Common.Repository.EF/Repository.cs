using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

        public void ExecuteCommand(string commandText, params object[] parameters)
        {
            UpdateNulltoDBNull(parameters);
            _dbContext.Database.ExecuteSqlCommand(commandText, parameters);
        }

        public void ExecuteNonQueryStoredProc(string spName, params object[] parameters)
        {
            string commandText = spName + " " + GetStoredProcParamsAsString(parameters);
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
        protected string GetStoredProcParamsAsString(IEnumerable<object> parameters)
        {
            if (parameters == null)
                return string.Empty;

            return string.Join(",", parameters.Cast<IDbDataParameter>().Select(x => string.Format("@{0}=@{0}" + (x.Direction == ParameterDirection.Output ? " OUTPUT" : string.Empty), x.ParameterName)));
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

    }
}