using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using FileStorage.Core.Entities;
using NUnit.Framework;
using Common.Repository.EF;

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
                //var fi = new FolderItem() {Name = "F1", Status = ItemStatus.Active, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now};
                //folderRep.Insert(fi);
                dbContext.SaveChanges();
                //var v2 = folderRep.Find(fi.ItemId);
            }
        }

        [Test]
        public void GetFreeFolderTest()
        {
            using (var dbContext = new FileStorageDbContext("MobileWalletConnection"))
            {
                //dbContext.FolderItems.ToList();
                var folder = dbContext.FolderItems.First(x => x.FolderItemId == 8);
                dbContext.Entry(folder).Reference(x => x.Parent).Load();
                var n =  dbContext.Entry(folder).Collection(x => x.ChildFolders).Query().Count();
                //v.ChildFolders = new Collection<FolderItem>();
                //v.ChildFolders.Add(new FolderItem(){Parent = v, Name = "123"});
                //dbContext.SaveChanges();
                //int? v = dbContext.GetFreeFolder(3, 3);
            }
        }
    }
}
