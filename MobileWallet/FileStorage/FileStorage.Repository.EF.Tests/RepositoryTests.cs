using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileStorage.Core.Entities;
using NUnit.Framework;

namespace FileStorage.Repository.EF.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        [Test]
        public void Test1()
        {
            using (var dbContext = new FileStorageDbContext("MobileWalletConnection"))
            {
                var folderRep = new Repository<FolderItem>(dbContext);
                var fi = new FolderItem() {Name = "F1", Status = ItemStatus.Active, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now};
                folderRep.Insert(fi);
                dbContext.SaveChanges();
                var v2 = folderRep.Find(fi.ItemId);
            }
        }
    }
}
