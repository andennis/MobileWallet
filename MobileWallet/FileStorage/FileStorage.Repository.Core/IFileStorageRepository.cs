using Common.Repository;
using FileStorage.Repository.Core.Entities;

namespace FileStorage.Repository.Core
{
    public interface IFileStorageRepository : IRepository<FolderItem>
    {
        FolderItem GetFreeFolderItem(int folderLevel, int maxItemsNumber);
        string GetFolderItemPath(int folderItemId);
        string GetStorageItemPath(int storageItemId);
        StorageItem GetStorageItem(int storageItemId);
        void ClearFileStorage();
    }
}
