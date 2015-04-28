using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassContentService : IPassManagerServiceBase<PassContent, PassContentFilter>
    {
        PassContent GetDetails(int entityId);
    }
}
