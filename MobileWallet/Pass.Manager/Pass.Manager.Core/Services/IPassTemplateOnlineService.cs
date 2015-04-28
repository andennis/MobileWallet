using Common.Repository;
using Common.Utils;

namespace Pass.Manager.Core.Services
{
    public interface IPassTemplateOnlineService
    {
        void Register(int passContentTempleteId);
        void Unregister(int passContentTempleteId);
        void UpdateOnline(int passContentTempleteId);
        void SetOnlineStatus(int passContentTempleteId, EntityStatus status);

        void RegisterProjectTempletes(int passProjectId);
        void UnregisterProjectTempletes(int passProjectId);
        void UpdateOnlineProjectTempletes(int passProjectId);

        FileContentInfo GetTemplateArchive(int entityId);
    }
}