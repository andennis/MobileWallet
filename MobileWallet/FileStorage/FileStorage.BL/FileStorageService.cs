using System;
using System.IO;
using FileStorage.Core;

namespace FileStorage.BL
{
    public class FileStorageService : IFileStorageService
    {
        public int PutFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public int PutFile(Stream stream)
        {
            throw new NotImplementedException();
        }

        public string GetFilePath(int itemId)
        {
            throw new NotImplementedException();
        }

        public Stream GetFile(int itemId)
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

        public void PurgeDeletedItems()
        {
            throw new NotImplementedException();
        }
    }
}
