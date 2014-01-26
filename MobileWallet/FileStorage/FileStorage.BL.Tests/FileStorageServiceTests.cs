using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileStorage.Core;
using FileStorage.Repository.EF;
using NUnit.Framework;

namespace FileStorage.BL.Tests
{
    [TestFixture]
    public class FileStorageServiceTests
    {
        private readonly IFileStorageConfig _fsConfig;

        public FileStorageServiceTests()
        {
            _fsConfig = new FileStorageConfig();
        }

        [Test]
        public void PutFileTest()
        {
            using (var dbSession = new FileStorageDbSession(_fsConfig))
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                fsService.PutFile("Data\\TextFile1.txt");
                fsService.PutFile("Data\\TextFile1.txt");
                fsService.PutFile("Data\\TextFile1.txt");
            }
            
        }

        [Test]
        public void PutFolderTest()
        {
            using (var dbSession = new FileStorageDbSession(_fsConfig))
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                fsService.PutFolder("Data", false);
            }
        }

        [Test]
        public void GetFilePathTest()
        {
            using (var dbSession = new FileStorageDbSession(_fsConfig))
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                string path = fsService.GetStorageItemPath(1);
            }
        }
    }
}
