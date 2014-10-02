using System;
using System.Linq.Expressions;

namespace Pass.Manager.Core
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int entityId);
        TEntity Get(int entityId);
        SearchResult<TEntity> Search(SearchContext searchContext, Expression<Func<TEntity, bool>> searchExpression = null);
    }
}
