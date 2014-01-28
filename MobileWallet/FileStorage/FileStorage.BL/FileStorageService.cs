using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Common.Repository;
using FileStorage.Core;
using FileStorage.Core.Entities;

namespace FileStorage.BL
{
    public class FileStorageService : IFileStorageService
    {
        private const string FolderItemPrefix = "FI";
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

        public int PutFile(string filePath, bool moveFile = false)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            //Get new file path
            FolderItem parentFolder;
            string dstFilePath = GetNewStorageItemPath(out parentFolder) + Path.GetExtension(filePath);

            //Copy\move file to specified location
            if (moveFile)
                File.Move(filePath, dstFilePath);
            else
                File.Copy(filePath, dstFilePath);

            //Save new storage item (file) to database
            var newFileItem = new StorageItem()
                            {
                                Name = Path.GetFileName(dstFilePath),
                                Status = ItemStatus.Active,
                                ItemType = StorageItemType.File,
                                OriginalName = Path.GetFileName(filePath),
                                Size = new FileInfo(dstFilePath).Length
                            };

            return CreateStorageItem(parentFolder, newFileItem);
        }
        public int PutFile(Stream fileStream)
        {
            if (fileStream == null)
                throw new ArgumentNullException("fileStream");

            //Get new file path
            string srcFileName = (fileStream is FileStream) ? ((FileStream)fileStream).Name : string.Empty;
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

            var newFileItem = new StorageItem()
            {
                Name = Path.GetFileName(dstFilePath),
                Status = ItemStatus.Active,
                ItemType = StorageItemType.File,
                OriginalName = srcFileName,
                Size = fileSize
            };

            return CreateStorageItem(parentFolder, newFileItem);
        }
        public int PutFolder(string folderPath, bool moveFolder = false)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentNullException("folderPath");

            //Get new file path
            FolderItem parentFolder;
            string dstPath = GetNewStorageItemPath(out parentFolder);

            //Copy\move folder to specified location
            if (moveFolder)
                Directory.Move(folderPath, dstPath);
            else
                DirectoryCopy(folderPath, dstPath, true);

            //Save new storage item (file) to database
            var newFileItem = new StorageItem()
            {
                Name = Path.GetFileName(dstPath),
                Status = ItemStatus.Active,
                ItemType = StorageItemType.Folder,
                //OriginalName = srcFileName,
                //Size = fileSize
            };

            return CreateStorageItem(parentFolder, newFileItem);
        }
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            var srcDirInfo = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = srcDirInfo.GetDirectories();

            if (!srcDirInfo.Exists)
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
                Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = srcDirInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, true);
                }
            }
        }
        public string GetStorageItemPath(int itemId)
        {
            string path = _fsUnitOfWork.FileStorageRepository.GetStorageItemPath(itemId);
            if (path == null)
                return null;

            path = GetPathWithoutRootFolder(path);
            return Path.Combine(_config.StoragePath, path);
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

            parentFolder.ChildFoldersCount += 1;
            var newFolder = new FolderItem() {Name = GenerateFolderName(parentFolder)};
            parentFolder.ChildFolders.Add(newFolder);
                
            _fsUnitOfWork.FileStorageRepository.Update(parentFolder);
            _fsUnitOfWork.Save();
            return newFolder;
        }

        private string GenerateFolderName(FolderItem parentFolder)
        {
            return FolderItemPrefix + parentFolder.ChildFoldersCount.ToString();
        }
    }
}
