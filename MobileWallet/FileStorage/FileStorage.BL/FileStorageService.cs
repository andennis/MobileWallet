using System;
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
        private const string FolderItemPrefix = "FI";
        private readonly IFileStorageConfig _config;
        private readonly IFileStorageRepository _fsRepository;

        public FileStorageService(IFileStorageConfig config, IFileStorageRepository fsRepository)
        {
            _config = config;
            _fsRepository = fsRepository;
        }

        private void InitFileStorage()
        {
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

        private FolderItem GetFreeFolder(int folderLevel)
        {
            FolderItem fi = GetFreeFolder(folderLevel, _config.MaxItemsNumber);
            if (fi != null)
            {
                if (folderLevel == _config.StorageDeep)
                    return fi;
                /*
                fi.ChildFoldersCount += 1;
                var newFolder = new FolderItem()
                         {
                             Parent = fi,
                             Name = FolderItemPrefix + fi.ChildFoldersCount,
                         };
                */
                _fsRepository.Insert(fi);
            }

            fi = GetFreeFolder(--folderLevel);
            if (fi == null)
            {
                //if (folderLevel == 1)

                
            }
            
            if (fi == null)
                throw new FileStorageException(string.Format("File storage is full with deep {0}. Increase the storage deep parameter in configuration"));

            return fi;
        }

        private FolderItem GetFreeFolder(int folderLevel, int maxItemsNumber)
        {
            return _fsRepository.SqlQuery(DbScheme + ".GetFreeFolder @FolderLevel, @MaxItemsNumber",
                                          new SqlParameter("FolderLevel", folderLevel), 
                                          new SqlParameter("MaxItemsNumber", maxItemsNumber)).FirstOrDefault();
        }

        private int GetItemsNumber(int folderLevel)
        {
            return 0;
        }
    }
}
