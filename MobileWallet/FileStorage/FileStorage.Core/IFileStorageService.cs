using FileStorage.Core.Entities;

namespace FileStorage.Core
{
    public interface IFileStorageService
    {
        int PutFile(string filePath);
        int PutFolder(string folderPath);
        ItemInfo GetItem(int itemId);
        void DeleteItem(int itemId);
    }
}
