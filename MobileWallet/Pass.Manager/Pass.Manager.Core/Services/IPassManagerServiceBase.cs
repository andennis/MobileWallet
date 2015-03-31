using Common.BL;

namespace Pass.Manager.Core.Services
{
    /*
    public interface IPassManagerServiceBase<TEntity, TSearchFilter> : IPassManagerServiceBase<TEntity, TEntity, TSearchFilter>
        where TEntity : class
        where TSearchFilter : SearchFilterBase
    {
    }
    */

    public interface IPassManagerServiceBase<TEntity, TSearchFilter> : IBaseService<TEntity, TSearchFilter>
        where TEntity : class
        //where TEntityView : class
        where TSearchFilter : SearchFilterBase
    {
        SearchResult<TEntityView> SearchView<TEntityView>(SearchContext searchContext, TSearchFilter searchFilter) where TEntityView : class;
    }
}