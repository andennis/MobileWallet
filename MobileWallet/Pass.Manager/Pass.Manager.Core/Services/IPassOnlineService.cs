using Common.Utils;

namespace Pass.Manager.Core.Services
{
    public interface IPassOnlineService
    {
        int Register(int passContentId);
        void UpdateOnline(int passContentId);
        FileContentInfo GetPassPackage(int passContentId);
    }
}