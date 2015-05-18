using System.Collections.Generic;

namespace Pass.Manager.Core.Repositories
{
    public interface IPassManagerDefaultRepository//<TEntity> : IRepository<TEntity> where TEntity : class
    {
        TEntityView GetView<TEntityView>(int entityId);
        IEnumerable<TEntityView> Search<TEntityView>(IDictionary<string, object> searchParams, out int totalRecords);
    }
}
