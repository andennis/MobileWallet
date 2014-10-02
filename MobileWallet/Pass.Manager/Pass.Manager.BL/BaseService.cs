﻿using System;
using System.Collections;
using System.Collections.Generic;
using Pass.Manager.Core;
using Common.Repository;
using System.Linq.Expressions;
using System.Linq;

namespace Pass.Manager.BL
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
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
        public virtual SearchResult<TEntity> Search(SearchContext searchContext, 
            Expression<Func<TEntity, bool>> searchExpression = null)
        {
            //TODO resolve the problem with paging
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
