using Common.Repository;
using FileStorage.Core.Entities;

namespace FileStorage.Core
{
    public interface IFileStorageRepository : IRepository<FolderItem>
    {
        FolderItem GetFreeFolderItem(int folderLevel, int maxItemsNumber);
        string GetFolderItemPath(int folderItemId);
        string GetStorageItemPath(int storageItemId);
        void ClearFileStorage();
    }
}
