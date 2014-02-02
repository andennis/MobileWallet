using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Common.Repository.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        protected UnitOfWork(IDbSession dbSession)
        {
            if (dbSession == null)
                throw new ArgumentNullException("dbSession");
            if (dbSession.DbContext == null)
                throw new ArgumentException("dbSession.DbContext should not be null");

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
        #endregion

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
            foreach (DbEntityEntry dbEntityEntry in _dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
            {
                //IEntityVersionable
                var entityVersionable = dbEntityEntry.Entity as IEntityVersionable;
                if (entityVersionable != null)
                {
                    entityVersionable.Version = 1;
                    entityVersionable.CreatedDate = DateTime.Now;
                    entityVersionable.UpdatedDate = entityVersionable.CreatedDate;
                }
            }
            foreach (DbEntityEntry dbEntityEntry in _dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified))
            {
                var entityVersionable = dbEntityEntry.Entity as IEntityVersionable;
                if (entityVersionable != null)
                {
                    entityVersionable.Version++;
                    entityVersionable.UpdatedDate = DateTime.Now;
                }
            }

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