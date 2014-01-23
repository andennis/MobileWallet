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
        private IFileStorageConfig _fsConfig;

        private class FileStorageServiceTest : FileStorageService
        {
            public FileStorageServiceTest(IFileStorageConfig config, IFileStorageRepository fsRepository) 
                :base(config, fsRepository)
            {
            }
        }

        public FileStorageServiceTests()
        {
            _fsConfig = new FileStorageConfig();
        }

        [Test]
        public void PutFileTest()
        {
            using (var dbContext = new FileStorageDbContext("MobileWalletConnection"))
            {
                var fsService = new FileStorageServiceTest(new FileStorageConfig(), new FileStorageRepository(dbContext));
                fsService.PutFile("");
                fsService.PutFile("");
                fsService.PutFile("");
                fsService.PutFile("");
            }
            
        }
    }
}
