using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pass.Manager.Core;
using Common.Repository;
using System.Linq;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.BL
{
    public abstract class BaseService<TEntity, TSearchFilter, TUnitOfWork> : IBaseService<TEntity, TSearchFilter>
        where TEntity : class, new()
        where TSearchFilter : SearchFilterBase
        where TUnitOfWork : IUnitOfWork
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly TUnitOfWork _unitOfWork;

        protected BaseService(TUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
        }

        public virtual void Create(TEntity entity)
        {
            _repository.Insert(entity);
            _unitOfWork.Save();
        }
        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
            _unitOfWork.Save();
        }
        public virtual void Delete(int entityId)
        {
            _repository.Delete(entityId);
            _unitOfWork.Save();
        }
        public virtual TEntity Get(int entityId)
        {
            return _repository.Find(entityId);
        }
        public virtual SearchResult<TEntity> Search(SearchContext searchContext, TSearchFilter searchFilter = null)
        {
            return Search(searchContext, x => true);
        }

        protected SearchResult<TEntity> Search(SearchContext searchContext, Expression<Func<TEntity, bool>> searchExpression = null)
        {
            //TODO paging
            //int totalCount;
            IEnumerable<TEntity> data = _repository.Query()
                .Filter(searchExpression)
                .Get();

            return new SearchResult<TEntity>()
            {
                Data = data,
                TotalCount = data.Count()
            };

        }

    }
}
