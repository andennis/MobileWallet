using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IPassTemplateStorageService
    {
        int CreateTemplateStorage();

        void PutBaseTemplateFiles(int templateStorageId, string srcTemplateFolderPath);
        void PutNativeTemplateFiles(int templateStorageId, ClientType clientType, string srcTemplateFolderPath);

        void GetBaseTemplateFiles(int templateStorageId, string dstFolderPath);
        void GetNativeTemplateFiles(int templateStorageId, ClientType clientType, string dstFolderPath);

        void DeleteTemplate(int templateStorageId);
    }
}