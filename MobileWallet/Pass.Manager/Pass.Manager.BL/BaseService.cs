using System;
using System.Collections;
using System.Collections.Generic;
using Pass.Manager.Core;
using Common.Repository;
using Pass.Manager.Core.Entities;
using System.Linq.Expressions;
using System.Linq;

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
        public virtual SearchResult<TEntity> Search(SearchContext searchContext, 
            Expression<Func<TEntity, bool>> searchExpression = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            if (orderBy == null)
                orderBy = q => q.OrderBy(x => x.EntityID);

            int totalCount;
            IEnumerable<TEntity> data = _repository.Query()
                .Filter(searchExpression)
                .OrderBy(orderBy)
                .GetPage(searchContext.PageIndex, searchContext.PageSize, out totalCount);

            return new SearchResult<TEntity>()
            {
                Data = data,
                TotalCount = totalCount
            };

        }
    }
}
