using Common.BL;
using Common.Utils;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassImageService : IBaseService<PassImage, PassImageFilter>
    {
        FileContentInfo GetImage(int imageId);
        PassImage GetDetails(int entityId);
    }
}