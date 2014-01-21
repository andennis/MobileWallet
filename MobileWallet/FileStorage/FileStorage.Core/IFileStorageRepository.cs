using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using FileStorage.Core.Entities;

namespace FileStorage.Core
{
    public interface IFileStorageRepository : IRepository<FolderItem>
    {
        //FolderItem GetFreeFolder(int itemLevel, int maxItemsNumber);
    }
}
