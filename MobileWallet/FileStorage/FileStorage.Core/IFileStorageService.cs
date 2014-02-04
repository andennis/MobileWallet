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

        void PutToStorageFolder(int itemId, string srcDirOrFilePath, bool move = false);
        void PutToStorageFolder(int itemId, string srcDirOrFilePath, string dstDirPath, bool move = false);

        void DeleteStorageItem(int itemId);
        void PurgeDeletedItems();
    }
}
