using System;
using System.Collections.ObjectModel;
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

            InitFileStorage();
        }

        private void InitFileStorage()
        {
            if (!_fsRepository.Query().Get().Any())
            {
                _fsRepository.Insert(new FolderItem(){Name = "\\"});
                _fsRepository.SaveChanges();
            }
        }

        public int PutFile(string filePath)
        {
            FolderItem fi = GetFreeFolder(_config.StorageDeep);
            return 0;
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

        protected FolderItem GetFreeFolder(int folderLevel)
        {
            FolderItem parentFolder = GetFreeFolder(folderLevel, _config.MaxItemsNumber);
            if (parentFolder == null)
            {
                if (folderLevel == 0)
                    throw new FileStorageException(string.Format("File storage is full. Increase the parameters storage deep or max items number in configuration"));

                parentFolder = GetFreeFolder(folderLevel - 1);
            }

            if (folderLevel == _config.StorageDeep)
                return parentFolder;

            var newFolder = GenerateNewFolder(parentFolder);
            parentFolder.ChildFolders = new Collection<FolderItem>(){newFolder};
            parentFolder.ChildFoldersCount += 1;
                
            _fsRepository.Update(parentFolder);
            _fsRepository.SaveChanges();
            return parentFolder.ChildFolders.First();
        }

        private FolderItem GetFreeFolder(int folderLevel, int maxItemsNumber)
        {
            return _fsRepository.SqlQuery(DbScheme + ".GetFreeFolder @FolderLevel, @MaxItemsNumber",
                                          new SqlParameter("FolderLevel", folderLevel), 
                                          new SqlParameter("MaxItemsNumber", maxItemsNumber)).FirstOrDefault();
        }

        private FolderItem GenerateNewFolder(FolderItem parentFolder)
        {
            if (parentFolder == null)
                return new FolderItem()
                    {
                        Name = FolderItemPrefix + "1"
                    };

            return new FolderItem()
                    {
                        Parent = parentFolder,
                        Name = FolderItemPrefix + parentFolder.ChildFoldersCount + 1
                    };
        }
    }
}
