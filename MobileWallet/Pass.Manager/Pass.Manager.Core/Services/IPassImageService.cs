using System.IO;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassImageService : IPassManagerServiceBase<PassImage, PassImageFilter>
    {
        FileContentInfo GetImage(int imageId);
    }
}