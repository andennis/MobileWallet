using Common.Repository;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Core.Services
{
    public interface IPassTemplateOnlineService
    {
        void Register(int passContentTempleteId);
        void Unregister(int passContentTempleteId);
        void UpdateOnlineTemplete(int passContentTempleteId);
        void SetOnlineStatus(int passContentTempleteId, EntityStatus status);

        void RegisterProjectTempletes(int passProjectId);
        void UnregisterProjectTempletes(int passProjectId);
        void UpdateOnlineProjectTempletes(int passProjectId);

        FileContentInfo GetTemplateArchive(int entityId);
    }
}