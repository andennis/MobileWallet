using Common.BL;

namespace Pass.Manager.Core.Services
{
    public interface IPassManagerServiceBase<TEntity, TSearchFilter> : IBaseService<TEntity, TSearchFilter>
        where TEntity : class
        where TSearchFilter : SearchFilterBase
    {
        TEntityView GetView<TEntityView>(int entityId);
        SearchResult<TEntityView> SearchView<TEntityView>(SearchContext searchContext, TSearchFilter searchFilter) where TEntityView : class;
    }
}