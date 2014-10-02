
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core
{
    public interface IBaseService<TEntity, TSearchFilter> 
        where TEntity : class
        where TSearchFilter : SearchFilterBase
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int entityId);
        TEntity Get(int entityId);
        SearchResult<TEntity> Search(SearchContext searchContext, TSearchFilter searchFilter = null);
    }
}
