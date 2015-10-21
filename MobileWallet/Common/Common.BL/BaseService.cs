using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Common.Extensions;
using Common.Repository;

namespace Common.BL
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
            Validate(entity);
            _repository.Insert(entity);
            _unitOfWork.Save();
        }
        public virtual void Update(TEntity entity)
        {
            Validate(entity);
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

        public virtual SearchResult<TEntity> Search(SearchContext searchContext, TSearchFilter searchFilter)
        {
            return Search(searchContext, x => true);
        }

        protected SearchResult<TEntity> Search(SearchContext searchContext, Expression<Func<TEntity, bool>> searchExpression)
        {
            int totalCount;
            IEnumerable<TEntity> data;
            
            if (searchContext.PageSize == 0)
            {
                data = _repository.Query()
                .Filter(searchExpression)
                .Get();
                totalCount = data.Count();
            }
            else if (searchContext.SortBy != null)
            {
                //For grid sorting
                var param = Expression.Parameter(typeof(TEntity), "p");
                var prop = Expression.Property(param, searchContext.SortBy);
                var exp = Expression.Lambda(prop, param);
                var paramExpr = Expression.Parameter(typeof(IQueryable<TEntity>));
                MethodInfo orderByMethodInfo = typeof(Queryable).GetMethods().First(method => method.Name == "OrderBy" && method.GetParameters().Count() == 2).MakeGenericMethod(typeof(TEntity), prop.Type);
                MethodInfo orderByDescMethodInfo = typeof(Queryable).GetMethods().First(method => method.Name == "OrderByDescending" && method.GetParameters().Count() == 2).MakeGenericMethod(typeof(TEntity), prop.Type);
                var orderByExpr = Expression.Call(searchContext.SortDirection == "asc" ? orderByMethodInfo : orderByDescMethodInfo, paramExpr, exp);
                var expr = Expression.Lambda<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(orderByExpr, paramExpr).Compile();

                data = _repository.Query()
                .Filter(searchExpression)
                .OrderBy(expr)
                .GetPage(searchContext.PageIndex, searchContext.PageSize, out totalCount);
            }
            else
            {
                data = _repository.Query()
                .Filter(searchExpression)
                .GetPage(searchContext.PageIndex, searchContext.PageSize, out totalCount);
            }
            

            return new SearchResult<TEntity>()
            {
                Data = data,
                TotalCount = totalCount
                //TotalCount = data.Count()
            };

        }

        public virtual TEntityView GetView<TEntityView>(int entityId) where TEntityView : class
        {
            return _repository.GetView<TEntityView>(entityId);
        }

        public virtual SearchResult<TEntityView> SearchView<TEntityView>(SearchContext searchContext, TSearchFilter searchFilter) where TEntityView : class
        {
            IEnumerable<QueryParameter> searchParams = searchFilter.ObjectPropertiesToDictionary().Select(x => new QueryParameter(){Name = x.Key, Value = x.Value});
            searchParams = searchParams.Union(searchContext.ObjectPropertiesToDictionary().Select(x => new QueryParameter(){Name = x.Key, Value = x.Value}));
            int totalRecords;
            IEnumerable<TEntityView> result = _repository.Search<TEntityView>(searchParams, out totalRecords);

            return new SearchResult<TEntityView>()
            {
                Data = result,
                TotalCount = totalRecords
            };
        }

        protected virtual void Validate(TEntity entity)
        {
        }

    }
}
