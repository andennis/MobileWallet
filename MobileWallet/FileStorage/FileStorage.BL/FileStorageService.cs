﻿using System;
using System.IO;
using FileStorage.Core;

namespace FileStorage.BL
{
    public sealed class FileStorageService : IFileStorageService
    {
        private readonly IFileStorageConfig _config;

        public FileStorageService(IFileStorageConfig config)
        {
            _config = config;
        }

        public int PutFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public int PutFile(Stream fileStream)
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

        private string GenerateItemName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
