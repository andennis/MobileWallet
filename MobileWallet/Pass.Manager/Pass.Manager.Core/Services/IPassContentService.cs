using Common.BL;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassContentService : IBaseService<PassContent, PassContentFilter>
    {
        PassContent GetDetails(int entityId);
        void SyncToTemplate(int entityId);
    }
}
