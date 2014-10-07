using System;
using System.IO;

namespace FileStorage.Core
{
    public interface IFileStorageService : IDisposable
    {
        /// <summary>
        /// Put file or directory content into storage
        /// </summary>
        /// <param name="fileOrDirPath"></param>
        /// <param name="move"></param>
        /// <returns>Storage item ID</returns>
        int Put(string fileOrDirPath, bool move = false);
        int Put(Stream fileStream);
        string GetStorageItemPath(int itemId);

        /// <summary>
        /// Create storage item as folder
        /// </summary>
        /// <param name="folderPath">The absolute path to the storage item (folder)</param>
        /// <returns>Storage item ID</returns>
        int CreateStorageFolder(out string folderPath);

        void PutToStorageFolder(int itemId, string srcFileOrDirPath, bool move = false);

        /// <summary>
        /// Put file or directory content into specified directory of storage item(folder)
        /// </summary>
        /// <param name="itemId">Storage item ID</param>
        /// <param name="srcFileOrDirPath">File or directory path</param>
        /// <param name="dstDirPath">directory path in scope of storage item(folder)</param>
        /// <param name="move">Move files and directories if True or copy if False</param>
        void PutToStorageFolder(int itemId, string srcFileOrDirPath, string dstDirPath, bool move = false);
        void ClearStorageFolder(int itemId);

        void DeleteStorageItem(int itemId);
        void PurgeDeletedItems();
    }
}
