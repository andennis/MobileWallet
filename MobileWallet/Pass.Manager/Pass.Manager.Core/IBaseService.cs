using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pass.Manager.Core
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        int Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int entityId);
        TEntity Get(int entityId);
        SearchResult<TEntity> Search(SearchContext searchContext,
            Expression<Func<TEntity, bool>> searchExpression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
    }
}
