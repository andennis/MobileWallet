﻿using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using FileStorage.Core;
using FileStorage.Core.Entities;

namespace FileStorage.BL
{
    public class FileStorageService : IFileStorageService
    {
        private const string DbScheme = "fs";
        private readonly IFileStorageConfig _config;
        private readonly IFileStorageRepository _fsRepository;

        public FileStorageService(IFileStorageConfig config, IFileStorageRepository fsRepository)
        {
            _config = config;
            _fsRepository = fsRepository;
        }

        private void InitFileStorage()
        {
            FolderItem fi = _fsRepository.Query().Filter(x => x.Parent == null).Get().FirstOrDefault();
            if (fi == null)
                _fsRepository.Insert(new FolderItem(){});
        }

        private FolderItem CreateFolderHierarchy(int folderLevel)
        {
            return null;
            //GetFreeFolder(folderLevel, )
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

        private FolderItem GetFreeFolder(int itemLevel, int maxItemsNumber)
        {
            return _fsRepository.SqlQuery(DbScheme + ".GetFreeFolder @ItemLevel, @MaxItemsNumber",
                                          new SqlParameter("ItemLevel", itemLevel), 
                                          new SqlParameter("MaxItemsNumber", maxItemsNumber)).FirstOrDefault();
        }
    }
}
