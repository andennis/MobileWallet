using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using FileStorage.Core;
using FileStorage.Core.Entities;
using Common.Repository.EF;

namespace FileStorage.Repository.EF
{
    public sealed class FileStorageRepository : Repository<FolderItem>, IFileStorageRepository
    {
        public FileStorageRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public FolderItem GetFreeFolderItem(int folderLevel, int maxItemsNumber)
        {
            return SqlQuery(FileStorageDbContext.DbScheme + ".GetFreeFolder @FolderLevel, @MaxItemsNumber",
                                          new SqlParameter("FolderLevel", folderLevel),
                                          new SqlParameter("MaxItemsNumber", maxItemsNumber)).FirstOrDefault();
        }

        public string GetFolderItemPath(int folderItemId)
        {
            return SqlQueryScalar<string>(FileStorageDbContext.DbScheme + ".GetFolderPath @FolderItemId",
                                          new SqlParameter("FolderItemId", folderItemId));
        }

        public string GetStorageItemPath(int storageItemId)
        {
            return SqlQueryScalar<string>(FileStorageDbContext.DbScheme + ".GetStorageItemPath @StorageItemId",
                                          new SqlParameter("StorageItemId", storageItemId));
        }

        public void ClearFileStorage()
        {
            using (var trn = new TransactionScope())
            {
                ExecuteCommand(string.Format("DELETE FROM {0}.StorageItem", FileStorageDbContext.DbScheme));
                ExecuteCommand(string.Format("DELETE FROM {0}.FolderItem", FileStorageDbContext.DbScheme));
                trn.Complete();
            }
        }
    }
}
