using Common.Utils;

namespace Pass.Manager.Core.Services
{
    public interface IPassOnlineService
    {
        void Register(int passContentId);
        void UpdateOnline(int passContentId);
        FileContentInfo GetPassPackage(int passContentId);
    }
}