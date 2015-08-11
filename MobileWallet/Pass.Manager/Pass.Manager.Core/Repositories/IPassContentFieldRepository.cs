using Common.Repository;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core.Repositories
{
    public interface IPassContentFieldRepository : IRepository<PassContentField>
    {
        PassContentFieldView GetView(int passContentId, int passProjectFieldId);
    }
}