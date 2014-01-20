using System.IO;

namespace FileStorage.Core
{
    public interface IFileStorageService
    {
        int PutFile(string filePath);
        int PutFile(Stream stream);
        string GetFilePath(int itemId);
        Stream GetFile(int itemId);
        void DeleteFile(int itemId);

        int PutFolder(string folderPath);
        string GetFolderPath(int itemId);
        void DeleteFolder(int itemId);

        void PurgeDeletedItems();
    }
}
