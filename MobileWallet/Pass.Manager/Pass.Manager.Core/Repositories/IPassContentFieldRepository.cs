using System.Collections.Generic;
using Common.Repository;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core.Repositories
{
    public interface IPassContentFieldRepository : IRepository<PassContentField>
    {
        IEnumerable<PassContentFieldView> GetListView(int passContentId);
        PassContentFieldView GetView(int passContentId, int passProjectFieldId);
    }
}