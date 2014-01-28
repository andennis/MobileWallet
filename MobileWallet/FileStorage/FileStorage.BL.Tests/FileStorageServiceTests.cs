using System;
using System.Collections.Generic;
using System.IO;
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
        private const string TestFilePath = @"Data\TextFile1.txt";
        private const string TestFolderPath = @"Data\F1";

        private readonly IFileStorageConfig _fsConfig;
        //private readonly IFileStorageUnitOfWork _fsUnitOfWork;

        public FileStorageServiceTests()
        {
            _fsConfig = new FileStorageConfig();
            //_fsUnitOfWork = new FileStorageUnitOfWork(dbSession)
        }

        [SetUp]
        public void InitEachTest()
        {
            ClearFileStorageFolder();
            ClearFileStorageDb();
        }

        [Test]
        public void PutFileTest()
        {
            using (var dbSession = new FileStorageDbSession(_fsConfig))
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                for (int i = 0; i < 16; i++)
                    fsService.PutFile(TestFilePath);

                Assert.Throws<FileStorageException>(() => fsService.PutFile(TestFilePath));
            }
        }

        [Test]
        public void PutFolderTest()
        {
            using (var dbSession = new FileStorageDbSession(_fsConfig))
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                for (int i = 0; i < 16; i++)
                    fsService.PutFolder(TestFolderPath);

                Assert.Throws<FileStorageException>(() => fsService.PutFolder(TestFolderPath));
            }
        }

        [Test]
        public void PutBothFoldersAndFilesTest()
        {
            using (var dbSession = new FileStorageDbSession(_fsConfig))
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                for (int i = 0; i < 8; i++)
                {
                    fsService.PutFolder(TestFolderPath);
                    fsService.PutFile(TestFilePath);
                }

                Assert.Throws<FileStorageException>(() => fsService.PutFolder(TestFolderPath));
                Assert.Throws<FileStorageException>(() => fsService.PutFile(TestFilePath));
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

        private void ClearFileStorageFolder()
        {
            string[] dirs = Directory.GetDirectories(_fsConfig.StoragePath);
            foreach (string dir in dirs)
                Directory.Delete(dir, true);

            string[] files = Directory.GetFiles(_fsConfig.StoragePath);
            foreach (string file in files)
                File.Delete(file);
        }
        private void ClearFileStorageDb()
        {
            using (var dbSession = new FileStorageDbSession(_fsConfig))
            {
                var uow = new FileStorageUnitOfWork(dbSession);
                uow.FileStorageRepository.ClearFileStorage();
            }
        }
    }
}
