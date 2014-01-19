using System;
using FileStorage.Core;

namespace FileStorage.BL
{
    public class FileStorageService : IFileStorageService
    {
        public int PutFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public int PutFile(System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }

        public string GetFilePath(int itemId)
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream GetFile(int itemId)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(int itemId)
        {
            throw new NotImplementedException();
        }

        public int PutFolder(string folderPath)
        {
            throw new NotImplementedException();
        }

        public string GetFolderPath(int itemId)
        {
            throw new NotImplementedException();
        }

        public void DeleteFolder(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
