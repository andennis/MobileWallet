using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IPassTemplateStorageService
    {
        void PutBaseTemplateFiles(int passTemplateId, string srcTemplateFolderPath);
        void PutNativeTemplateFiles(int passTemplateId, ClientType clientType, string srcTemplateFolderPath);
        void GetBaseTemplateFiles(int passTemplateId, string dstFolderPath);
        void GetNativeTemplateFiles(int passTemplateId, ClientType clientType, string dstFolderPath);
    }
}