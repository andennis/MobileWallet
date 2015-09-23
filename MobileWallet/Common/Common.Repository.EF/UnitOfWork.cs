using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace Common.Repository.EF
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContextBase _dbContext;
        protected readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        protected UnitOfWork(DbContextBase dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            _dbContext = dbContext;
        }

        protected abstract HashSet<Type> AllowedRepositoryEntities { get; }

        protected void RegisterCustomRepository<TEntity>(IRepository<TEntity> repository) where TEntity : class
        {
            Type entityType = typeof (TEntity);
            if (_repositories.ContainsKey(entityType))
                throw new Exception(string.Format("Repository has already been registered for the the entity type: {0}", entityType.Name));

            //TODO Lazy should be removed
            var lazyRep = new Lazy<IRepository<TEntity>>(() => repository);
            _repositories.Add(entityType, lazyRep);
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

        public virtual IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            Type entityType = typeof(TEntity);

            object repository;
            if (_repositories.TryGetValue(entityType, out repository))
            {
                var lazyRep = repository as Lazy<IRepository<TEntity>>;
                if (lazyRep != null)
                    return lazyRep.Value;

                return (IRepository<TEntity>)repository;
            }

            if (!AllowedRepositoryEntities.Contains(entityType))
                throw new Exception(string.Format("Repository<{0}> has not been registered for the UnitOfType", entityType.Name));

            object rep = CreateDefaultRepository(typeof(TEntity));
            _repositories.Add(entityType, rep);
            return (IRepository<TEntity>)rep;
        }

        protected virtual object CreateDefaultRepository(Type entityType)
        {
            Type repositoryType = typeof(Repository<>);
            return Activator.CreateInstance(repositoryType.MakeGenericType(entityType), _dbContext);
        }

        public void Save()
        {
            foreach (DbEntityEntry dbEntityEntry in _dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
            {
                //IEntityVersionable
                var entityVersionable = dbEntityEntry.Entity as IEntityVersionable;
                if (entityVersionable != null)
                {
                    entityVersionable.CreatedDate = DateTime.Now;
                    entityVersionable.UpdatedDate = entityVersionable.CreatedDate;
                }
            }
            foreach (DbEntityEntry dbEntityEntry in _dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified))
            {
                var entityVersionable = dbEntityEntry.Entity as IEntityVersionable;
                if (entityVersionable != null)
                {
                    entityVersionable.UpdatedDate = DateTime.Now;
                }
            }

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }


        /*
        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            return _dbContext.GetValidationErrors();
        }
        */
    }
}