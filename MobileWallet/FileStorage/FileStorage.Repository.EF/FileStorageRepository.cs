using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage.Core;
using FileStorage.Core.Entities;
using Common.Repository.EF;

namespace FileStorage.Repository.EF
{
    public sealed class FileStorageRepository : Repository<FolderItem>, IFileStorageRepository
    {
        //private const string DbScheme = "fs";
        //private readonly FileStorageDbContext _dbContext;

        public FileStorageRepository(FileStorageDbContext dbContext)
            :base(dbContext)
        {
        }

        /*
        public FolderItem GetFreeFolder(int itemLevel, int maxItemsNumber)
        {
            return ((FileStorageDbContext) _dbContext).GetFreeFolder(itemLevel, maxItemsNumber);
        }
        */
    }
}
