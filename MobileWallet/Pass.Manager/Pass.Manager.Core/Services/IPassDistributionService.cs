using Common.Utils;

namespace Pass.Manager.Core.Services
{
    public interface IPassDistributionService
    {
        string GetPassCode(int passContentId);
        string GetPassTemplateCode(int passContentTemplateId);
        FileContentInfo GetPassPackage(string passCode);
    }
}