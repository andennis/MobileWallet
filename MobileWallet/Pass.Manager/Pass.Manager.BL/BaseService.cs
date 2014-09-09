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
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IEntityWithID, new()
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
        }

        public virtual int Create(TEntity entity)
        {
            _repository.Insert(entity);
            _unitOfWork.Save();
            return entity.EntityID;
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
        public virtual SearchResult<TEntity> Search(SearchContext searchContext, 
            Expression<Func<TEntity, bool>> searchExpression = null)
        {
            int totalCount;
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
