using System;
using System.Collections.Generic;
using Pass.Manager.Core;
using Common.Repository;
using Pass.Manager.Core.Entities;
using System.Linq.Expressions;

namespace Pass.Manager.BL
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IEntityWithID
    {
        protected IRepository<TEntity> _repository;

        protected BaseService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual int Create(TEntity entity)
        {
            _repository.Insert(entity);
            return entity.EntityID;
        }
        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
        public virtual void Delete(int entityId)
        {
            _repository.Delete(entityId);
        }
        public virtual TEntity Get(int entityId)
        {
            return _repository.Find(entityId);
        }
        public virtual IEnumerable<TEntity> Search(SearchContext searchContext, Expression<Func<TEntity, bool>> searchExpression)
        {
            int totalCount;
            return _repository.Query()
                .Filter(searchExpression)
                .GetPage(searchContext.PageIndex, searchContext.PageSize, out totalCount);
        }
    }
}
