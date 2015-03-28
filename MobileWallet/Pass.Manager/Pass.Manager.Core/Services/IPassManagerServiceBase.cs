using Common.BL;

namespace Pass.Manager.Core.Services
{
    public interface IPassManagerServiceBase<TEntity, TSearchFilter> : IPassManagerServiceBase<TEntity, TEntity, TSearchFilter>
        where TEntity : class
        where TSearchFilter : SearchFilterBase
    {
    }

    public interface IPassManagerServiceBase<TEntity, TEntityView, TSearchFilter> : IBaseService<TEntity, TSearchFilter>
        where TEntity : class
        where TEntityView : class
        where TSearchFilter : SearchFilterBase
    {
        SearchResult<TEntityView> SearchView(SearchContext searchContext, TSearchFilter searchFilter);
    }
}