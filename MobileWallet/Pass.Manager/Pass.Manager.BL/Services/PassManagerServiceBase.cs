using System;
using System.Collections.Generic;
using System.Linq;
using Common.BL;
using Common.Extensions;
using Pass.Manager.Core;
using Pass.Manager.Core.Services;
using Pass.Manager.Repository.EF;

namespace Pass.Manager.BL.Services
{
    public abstract class PassManagerServiceBase<TEntity, TSearchFilter> : PassManagerServiceBase<TEntity, TEntity, TSearchFilter>
        where TEntity : class, new()
        where TSearchFilter : SearchFilterBase
    {
        protected PassManagerServiceBase(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }

    public abstract class PassManagerServiceBase<TEntity, TEntityView, TSearchFilter> : BaseService<TEntity, TSearchFilter, IPassManagerUnitOfWork>, IPassManagerServiceBase<TEntity, TEntityView, TSearchFilter> 
        where TEntity : class, new()
        where TEntityView : class, new()
        where TSearchFilter : SearchFilterBase
    {
        protected PassManagerServiceBase(IPassManagerUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public SearchResult<TEntityView> SearchView(SearchContext searchContext, TSearchFilter searchFilter)
        {
            IDictionary<string, object> searchParams = searchFilter.ObjectPropertiesToDictionary();
            searchParams = searchParams.Union(searchContext.ObjectPropertiesToDictionary()).ToDictionary(k => k.Key, v => v.Value ?? DBNull.Value);
            IEnumerable<TEntityView> result = ((PassManagerDefaultRepository<TEntity>)_repository).Search<TEntityView>(searchParams);//.ToList();???

            return new SearchResult<TEntityView>()
                    {
                        Data = result,
                        TotalCount = result.Count()
                    };
        }
    }
}
