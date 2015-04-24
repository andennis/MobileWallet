﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Common.Repository;
using Common.Utils;
using FileStorage.Core;
using FileStorage.Core.Entities;
using FileStorage.Repository.Core;
using FileStorage.Repository.Core.Entities;

namespace FileStorage.BL
{
    public class FileStorageService : IFileStorageService
    {
        private const string RootFolderName = "FSRoot";
        private readonly IFileStorageConfig _config;
        private readonly IFileStorageUnitOfWork _fsUnitOfWork;

        public FileStorageService(IFileStorageConfig config, IFileStorageUnitOfWork fsUnitOfWork)
        {
            _config = config;
            _fsUnitOfWork = fsUnitOfWork;

            InitFileStorage();
        }

        private void InitFileStorage()
        {
            if (!_fsUnitOfWork.FileStorageRepository.Query().Get().Any())
            {
                _fsUnitOfWork.FileStorageRepository.Insert(new FolderItem() { Name = RootFolderName });
                _fsUnitOfWork.Save();
            }
        }

        public int Put(string fileOrDirPath, bool move = false)
        {
            if (string.IsNullOrEmpty(fileOrDirPath))
                throw new ArgumentNullException("fileOrDirPath");

            FolderItem parentFolder;
            string dstPath = GetNewStorageItemPath(out parentFolder);

            StorageItem newStorageItem;
            bool isDir = (File.GetAttributes(fileOrDirPath) & FileAttributes.Directory) == FileAttributes.Directory;
            if (isDir)
            {
                newStorageItem = PutFolder(fileOrDirPath, dstPath, move);
            }
            else
            {
                dstPath += Path.GetExtension(fileOrDirPath);
                newStorageItem = PutFile(fileOrDirPath, dstPath, move);
            }

            return CreateStorageItem(parentFolder, newStorageItem);
        }
        public int Put(Stream fileStream, string fileName = null)
        {
            if (fileStream == null)
                throw new ArgumentNullException("fileStream");

            //Get new file path
            string srcFileName = Path.GetFileName(fileName) ?? ((fileStream is FileStream) ? ((FileStream)fileStream).Name : string.Empty);
            FolderItem parentFolder;
            string dstFilePath = GetNewStorageItemPath(out parentFolder) + Path.GetExtension(srcFileName);

            //Copy file to specified location
            using (var newFileStream = new FileStream(dstFilePath, FileMode.CreateNew))
            {
                fileStream.CopyTo(newFileStream);
            }

            //Save new storage item (file) to database
            long fileSize = 0;
            try
            {
                fileSize = fileStream.Length;
            }
            catch (NotSupportedException)
            {
            }

            var newStorageItem = new StorageItem()
            {
                Name = Path.GetFileName(dstFilePath),
                Status = ItemStatus.Active,
                ItemType = StorageItemType.File,
                OriginalName = Path.GetFileName(srcFileName),
                Size = fileSize
            };

            return CreateStorageItem(parentFolder, newStorageItem);
        }
        private StorageItem PutFile(string srcFilePath, string dstFilePath, bool moveFile = false)
        {
            //Copy\move file to specified location
            if (moveFile)
                MoveFile(srcFilePath, dstFilePath);
            else
                File.Copy(srcFilePath, dstFilePath);

            return new StorageItem()
                    {
                        Name = Path.GetFileName(dstFilePath),
                        Status = ItemStatus.Active,
                        ItemType = StorageItemType.File,
                        OriginalName = Path.GetFileName(srcFilePath),
                        Size = new FileInfo(dstFilePath).Length
                    };
        }
        private StorageItem PutFolder(string srcFolderPath, string dstFolderPath, bool moveFolder = false)
        {
            //Copy\move folder to specified location
            if (moveFolder)
                MoveDirectory(srcFolderPath, dstFolderPath);
            else
                FileHelper.DirectoryCopy(srcFolderPath, dstFolderPath, true, true);

            return new StorageItem()
                    {
                        Name = Path.GetFileName(dstFolderPath),
                        Status = ItemStatus.Active,
                        ItemType = StorageItemType.Folder,
                        //OriginalName = srcFileName,
                        //Size = fileSize
                    };
        }

        public string GetStorageItemPath(int itemId)
        {
            string path = _fsUnitOfWork.FileStorageRepository.GetStorageItemPath(itemId);
            if (path == null)
                return null;

            path = GetPathWithoutRootFolder(path);
            return Path.Combine(_config.StoragePath, path);
        }

        public StorageFileInfo GetFile(int itemId, bool fileStream = false)
        {
            StorageItem si =  _fsUnitOfWork.FileStorageRepository.GetStorageItem(itemId);
            if (si == null)
                return null;

            var sfi = new StorageFileInfo()
            {
                Id = si.StorageItemId,
                Name = si.OriginalName ?? si.Name,
                Size = si.Size
            };

            if (fileStream)
            {
                string path = GetStorageItemPath(itemId);
                sfi.FileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            }

            return sfi;
        }

        public int CreateStorageFolder(out string folderPath)
        {
            //Get new file path
            FolderItem parentFolder;
            folderPath = GetNewStorageItemPath(out parentFolder);

            Directory.CreateDirectory(folderPath);

            //Save new storage item (folder) to database
            var newFileItem = new StorageItem()
            {
                Name = Path.GetFileName(folderPath),
                Status = ItemStatus.Active,
                ItemType = StorageItemType.Folder,
                //OriginalName = srcFileName,
                //Size = fileSize
            };

            return CreateStorageItem(parentFolder, newFileItem);
        }

        public void PutToStorageFolder(int itemId, string srcFileOrDirPath, bool move = false)
        {
            PutToStorageFolder(itemId, srcFileOrDirPath, null, move);
        }
        public void PutToStorageFolder(int itemId, string srcFileOrDirPath, string dstDirPath, bool move = false)
        {
            string dstStorageFolder = GetStorageItemPath(itemId);
            dstDirPath = Path.Combine(dstStorageFolder, dstDirPath ?? string.Empty);
            Directory.CreateDirectory(dstDirPath);
            bool isDir = (File.GetAttributes(srcFileOrDirPath) & FileAttributes.Directory) == FileAttributes.Directory;
            if (isDir)
            {
                dstDirPath = Path.Combine(dstDirPath, Path.GetFileName(srcFileOrDirPath));
                if (move)
                    MoveDirectory(srcFileOrDirPath, dstDirPath);
                else
                    FileHelper.DirectoryCopy(srcFileOrDirPath, dstDirPath, true, true);
            }
            else
            {
                string fileName = Path.GetFileName(srcFileOrDirPath);
                dstDirPath = Path.Combine(dstDirPath, fileName);
                if (move)
                    MoveFile(srcFileOrDirPath, dstDirPath);
                else
                    File.Copy(srcFileOrDirPath, dstDirPath);
            }
        }
        public void ClearStorageFolder(int itemId)
        {
            string dstStorageFolder = GetStorageItemPath(itemId);
            Directory.Delete(dstStorageFolder, true);
            Directory.CreateDirectory(dstStorageFolder);
        }

        public void DeleteStorageItem(int itemId)
        {
            IRepository<StorageItem> siRep = _fsUnitOfWork.GetRepository<StorageItem>();
            StorageItem si = siRep.Find(itemId);
            if (si == null)
                return;

            si.Status = ItemStatus.Deleted;
            siRep.Update(si);
            _fsUnitOfWork.Save();
        }
        public void PurgeDeletedItems()
        {
            throw new NotImplementedException();
        }

        private void MoveFile(string src, string dst)
        {
            if (ArePathsOnEqualDrives(src, dst))
            {
                File.Move(src, dst);
            }
            else
            {
                File.Copy(src, dst);
                File.Delete(src);
            }
        }

        private void MoveDirectory(string src, string dst)
        {
            if (ArePathsOnEqualDrives(src, dst))
            {
                Directory.Move(src, dst);
            }
            else
            {
                FileHelper.DirectoryCopy(src, dst, true, true);
                Directory.Delete(src, true);
            }
        }

        private bool ArePathsOnEqualDrives(string path1, string path2)
        {
            return (Directory.GetDirectoryRoot(path1) == Directory.GetDirectoryRoot(path2));
        }

        private string GetNewStorageItemPath(out FolderItem parentFolder)
        {
            //Get the folder where the file should be placed
            parentFolder = GetOrCreateFreeFolder(_config.StorageDeep);

            //Create the folder path
            string folderPath = _fsUnitOfWork.FileStorageRepository.GetFolderItemPath(parentFolder.FolderItemId);
            folderPath = GetPathWithoutRootFolder(folderPath);
            string dstFolderPath = Path.Combine(_config.StoragePath, folderPath);
            if (!Directory.Exists(dstFolderPath))
                Directory.CreateDirectory(dstFolderPath);

            //Generate file\folder name as GUID
            string fileName = Guid.NewGuid().ToString();
            return Path.Combine(dstFolderPath, fileName);
        }
        private int CreateStorageItem(FolderItem parentFolder, StorageItem newItem)
        {
            if (parentFolder.ChildStorageItems == null)
                parentFolder.ChildStorageItems = new Collection<StorageItem>();

            parentFolder.ChildStorageItems.Add(newItem);
            _fsUnitOfWork.FileStorageRepository.Update(parentFolder);
            _fsUnitOfWork.Save();

            return newItem.StorageItemId;
        }

        private string GetPathWithoutRootFolder(string folderPath)
        {
            return folderPath.Substring(RootFolderName.Length + 1, folderPath.Length - RootFolderName.Length - 1);
        }

        private FolderItem GetOrCreateFreeFolder(int folderLevel)
        {
            FolderItem parentFolder = _fsUnitOfWork.FileStorageRepository.GetFreeFolderItem(folderLevel, _config.MaxItemsNumber);
            if (parentFolder == null)
            {
                if (folderLevel == 0)
                    throw new FileStorageException(string.Format("File storage is full or has not been initialized correctly"));

                parentFolder = GetOrCreateFreeFolder(folderLevel - 1);
            }

            if (folderLevel == _config.StorageDeep)
                return parentFolder;

            if (parentFolder.ChildFolders == null)
                parentFolder.ChildFolders = new Collection<FolderItem>();

            var newFolder = new FolderItem() {Name = GenerateFolderName()};
            parentFolder.ChildFolders.Add(newFolder);
                
            _fsUnitOfWork.FileStorageRepository.Update(parentFolder);
            _fsUnitOfWork.Save();
            return newFolder;
        }

        private string GenerateFolderName()
        {
            return FileHelper.GetRandomFolderName();
        }

        #region IDisposable
        public void Dispose()
        {
            _fsUnitOfWork.Dispose();
        }
        #endregion
    }
}
