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
    public abstract class PassManagerServiceBase<TEntity, TSearchFilter> : BaseService<TEntity, TSearchFilter, IPassManagerUnitOfWork>, IPassManagerServiceBase<TEntity, TSearchFilter> 
        where TEntity : class, new()
        where TSearchFilter : SearchFilterBase
    {
        protected PassManagerServiceBase(IPassManagerUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public virtual SearchResult<TEntityView> SearchView<TEntityView>(SearchContext searchContext, TSearchFilter searchFilter) where TEntityView : class
        {
            IDictionary<string, object> searchParams = searchFilter.ObjectPropertiesToDictionary();
            searchParams = searchParams.Union(searchContext.ObjectPropertiesToDictionary()).ToDictionary(k => k.Key, v => v.Value ?? DBNull.Value);
            int totalRecords;
            IEnumerable<TEntityView> result = ((PassManagerDefaultRepository<TEntity>)_repository).Search<TEntityView>(searchParams, out totalRecords);//.ToList();???

            return new SearchResult<TEntityView>()
                    {
                        Data = result,
                        TotalCount = totalRecords
                    };
        }
    }
}
