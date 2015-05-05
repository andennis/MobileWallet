using Common.Utils;

namespace Pass.Manager.Core.Services
{
    public interface IPassDistributionService
    {
        string GetPassToken(int passContentId);
        string GetPassTemplateToken(int passContentTemplateId);
        FileContentInfo GetPassPackage(string token);
    }
}