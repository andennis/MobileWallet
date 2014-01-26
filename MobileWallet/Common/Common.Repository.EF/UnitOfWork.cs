using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Common.Repository.EF
{
    public abstract class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _dbContext;
        private readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        protected UnitOfWork(IDbSession dbSession)
        {
            _dbContext = (DbContext)dbSession.DbContext;
        }

        #region Dispose
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }
        #endregion Constuctor/Dispose

        protected void AddRepository<TEntity>(IRepository<TEntity> repository) where TEntity : class
        {
            Type type = typeof(IRepository<TEntity>);
            if (_repositories.ContainsKey(type))
                throw new Exception(string.Format("Repository type '{0}' already exits", type.Name));

            _repositories.Add(type, repository);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            Type type = typeof(IRepository<TEntity>);
            object repository;
            if (!_repositories.TryGetValue(type, out repository))
                throw new Exception(string.Format("Repository type '{0}' does not exist", type.Name));

            return (IRepository<TEntity>)repository;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        /*
        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            return _dbContext.GetValidationErrors();
        }
        */
    }
}