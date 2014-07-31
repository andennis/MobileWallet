
namespace Common.Repository
{
    public abstract class RepositoryTestBase<TUnitOfWork>
        where TUnitOfWork : class, IUnitOfWork
    {
        protected abstract TUnitOfWork CreateUnitOfWork();

        public void CreateEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            using (TUnitOfWork unitOfWork = CreateUnitOfWork())
            {
                var entityRep = unitOfWork.GetRepository<TEntity>();
                entityRep.Insert(entity);
                unitOfWork.Save();
            }
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            using (TUnitOfWork unitOfWork = CreateUnitOfWork())
            {
                var entityRep = unitOfWork.GetRepository<TEntity>();
                entityRep.Update(entity);
                unitOfWork.Save();
            }
        }

        public void DeleteEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            using (TUnitOfWork unitOfWork = CreateUnitOfWork())
            {
                var entityRep = unitOfWork.GetRepository<TEntity>();
                entityRep.Delete(entity);
                unitOfWork.Save();
            }
        }

        public TEntity ReadEntity<TEntity>(params object[] keys) where TEntity : class, new()
        {
            using (TUnitOfWork unitOfWork = CreateUnitOfWork())
            {
                var entityRep = unitOfWork.GetRepository<TEntity>();
                return entityRep.Find(keys);
            }
        }

    }
}
