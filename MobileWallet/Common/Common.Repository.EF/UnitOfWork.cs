using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Common.Repository.EF
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext _dbContext;
        protected readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        protected UnitOfWork(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            _dbContext = dbContext;
        }

        protected abstract HashSet<Type> AllowedRepositoryEntities { get; }

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

        public virtual IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new()
        {
            Type type = typeof(TEntity);
            if (!AllowedRepositoryEntities.Contains(type))
                throw new Exception(string.Format("Repository<{0}> has not been registered for the UnitOfType", type.Name));

            object repository;
            if (_repositories.TryGetValue(type, out repository))
                return (IRepository<TEntity>)repository;

            var repositoryType = typeof(Repository<>);
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext));

            return (IRepository<TEntity>)_repositories[type];
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