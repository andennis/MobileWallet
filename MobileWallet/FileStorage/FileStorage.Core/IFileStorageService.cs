using System.IO;

namespace FileStorage.Core
{
    public interface IFileStorageService
    {
        int PutFile(string filePath, bool moveFile = false);
        int PutFile(Stream fileStream);
        int PutFolder(string srcFolderPath, bool moveFolder = false);
        string GetStorageItemPath(int itemId);
        int CreateStorageFolder(out string folderPath);

        void DeleteStorageItem(int itemId);
        void PurgeDeletedItems();
    }
}
