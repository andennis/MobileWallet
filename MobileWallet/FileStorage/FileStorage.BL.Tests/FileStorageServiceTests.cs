using System;
using System.IO;
using System.Linq;
using Common.Repository;
using FileStorage.Core;
using FileStorage.Repository.EF;
using NUnit.Framework;

namespace FileStorage.BL.Tests
{
    [TestFixture]
    public class FileStorageServiceTests
    {
        private const string TestFolderBase = "Data";
        private const string TestFilePath = @"Data\TextFile1.txt";
        private const string TestFolderPath = @"Data\F1";

        private readonly IFileStorageConfig _fsConfig;

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
        public void FillStorageWithFilesTest()
        {
            using (IDbSession dbSession = CreateDbSession())
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                for (int i = 0; i < 16; i++)
                    Assert.Greater(fsService.PutFile(TestFilePath), 0);

                Assert.Throws<FileStorageException>(() => fsService.PutFile(TestFilePath));
                int count = GetFileCount(_fsConfig.StoragePath);
                Assert.AreEqual(16, count);
            }
        }

        [Test]
        public void FillStorageWithFoldersTest()
        {
            using (IDbSession dbSession = CreateDbSession())
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                for (int i = 0; i < 16; i++)
                    Assert.Greater(fsService.PutFolder(TestFolderPath), 0);

                Assert.Throws<FileStorageException>(() => fsService.PutFolder(TestFolderPath));
                int count = GetFolderCount(_fsConfig.StoragePath);
                Assert.AreEqual(30, count);
            }
        }

        [Test]
        public void FillStorageWithFoldersAndFilesTest()
        {
            using (IDbSession dbSession = CreateDbSession())
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                for (int i = 0; i < 8; i++)
                {
                    Assert.Greater(fsService.PutFolder(TestFolderPath), 0); 
                    Assert.Greater(fsService.PutFile(TestFilePath), 0);
                }

                Assert.Throws<FileStorageException>(() => fsService.PutFolder(TestFolderPath));
                Assert.Throws<FileStorageException>(() => fsService.PutFile(TestFilePath));

                int count = GetFolderCount(_fsConfig.StoragePath);
                Assert.AreEqual(22, count);
                count = GetFileCount(_fsConfig.StoragePath);
                Assert.AreEqual(16, count);
            }
        }

        [Test]
        public void PutFolderWithCopyTest()
        {
            string srcPath = Path.Combine(TestFolderBase, "F2");
            if (!Directory.Exists(srcPath))
                Directory.CreateDirectory(srcPath);

            string srcFilePath = Path.Combine(srcPath, "TextFile1.txt");
            File.Copy(TestFilePath, srcFilePath, true);

            using (IDbSession dbSession = CreateDbSession())
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                int id = fsService.PutFolder(srcPath);
                string dstPath = fsService.GetStorageItemPath(id);
                Assert.IsNotNullOrEmpty(dstPath);
                Assert.True(Directory.Exists(dstPath));
                string dstFilePath = Path.Combine(dstPath, "TextFile1.txt");
                Assert.True(File.Exists(dstFilePath));

                Assert.True(Directory.Exists(srcPath));
                Assert.True(File.Exists(dstFilePath));
            }
        }

        [Test]
        public void PutFolderWithMoveTest()
        {
            string srcPath = Path.Combine(TestFolderBase, "F2");
            if (!Directory.Exists(srcPath))
                Directory.CreateDirectory(srcPath);

            string srcFilePath = Path.Combine(srcPath, "TextFile1.txt");
            File.Copy(TestFilePath, srcFilePath, true);

            using (IDbSession dbSession = CreateDbSession())
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                int id = fsService.PutFolder(srcPath, true);
                string dstPath = fsService.GetStorageItemPath(id);
                Assert.IsNotNullOrEmpty(dstPath);
                Assert.True(Directory.Exists(dstPath));
                string dstFilePath = Path.Combine(dstPath, "TextFile1.txt");
                Assert.True(File.Exists(dstFilePath));

                Assert.False(Directory.Exists(srcPath));
                Assert.True(File.Exists(dstFilePath));
            }
        }

        [Test]
        public void PutFileWithCopyTest()
        {
            string srcFilePath = Path.Combine(TestFolderBase, "TextFile2.txt");
            File.Copy(TestFilePath, srcFilePath, true);

            using (IDbSession dbSession = CreateDbSession())
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                int id = fsService.PutFile(srcFilePath);
                string dstFilePath = fsService.GetStorageItemPath(id);
                Assert.IsNotNullOrEmpty(dstFilePath);
                Assert.True(File.Exists(dstFilePath));
                Assert.True(File.Exists(srcFilePath));
            }
        }

        [Test]
        public void PutFileWithMoveTest()
        {
            string srcFilePath = Path.Combine(TestFolderBase, "TextFile2.txt");
            File.Copy(TestFilePath, srcFilePath, true);

            using (IDbSession dbSession = CreateDbSession())
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                int id = fsService.PutFile(srcFilePath, true);
                string dstFilePath = fsService.GetStorageItemPath(id);
                Assert.IsNotNullOrEmpty(dstFilePath);
                Assert.True(File.Exists(dstFilePath));
                Assert.False(File.Exists(srcFilePath));
            }
        }

        [Test]
        public void PutFileByStreamTest()
        {
            using (Stream fs = new FileStream(TestFilePath, FileMode.Open))
            using (IDbSession dbSession = CreateDbSession())
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                int id = fsService.PutFile(fs);
                Assert.Greater(id, 0);
                string dstFilePath = fsService.GetStorageItemPath(id);
                Assert.IsNotNullOrEmpty(dstFilePath);
                Assert.True(File.Exists(dstFilePath));
            }
        }

        [Test]
        public void GetFilePathTest()
        {
            using (IDbSession dbSession = CreateDbSession())
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));
                int id = fsService.PutFile(TestFilePath);
                string path = fsService.GetStorageItemPath(id);
                Assert.IsNotNullOrEmpty(path);
                Assert.True(path.StartsWith(_fsConfig.StoragePath));
                Assert.True(File.Exists(path));

                id = fsService.PutFolder(TestFolderPath);
                path = fsService.GetStorageItemPath(id);
                Assert.IsNotNullOrEmpty(path);
                Assert.True(path.StartsWith(_fsConfig.StoragePath));
                Assert.True(Directory.Exists(path));
            }
        }

        [Test]
        public void DeleteStorageItemTest()
        {
            using (IDbSession dbSession = CreateDbSession())
            {
                var fsService = new FileStorageService(_fsConfig, new FileStorageUnitOfWork(dbSession));

                int id = fsService.PutFile(TestFilePath);
                string path = fsService.GetStorageItemPath(id);
                Assert.IsNotNullOrEmpty(path);
                Assert.True(File.Exists(path));

                fsService.DeleteStorageItem(id);
                Assert.IsNull(fsService.GetStorageItemPath(id));
                Assert.True(File.Exists(path));

                Assert.DoesNotThrow(() => fsService.DeleteStorageItem(id));
            }
        }

        [Ignore]
        public void PurgeDeletedItemsTests()
        {
            throw new NotImplementedException();
        }

        private IDbSession CreateDbSession()
        {
            return new FileStorageDbSession(_fsConfig);
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

        private int GetFolderCount(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            return dirs.Length + dirs.Sum(dir => GetFolderCount(Path.Combine(path, dir)));
        }
        private int GetFileCount(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);
            return files.Length + dirs.Sum(dir => GetFileCount(Path.Combine(path, dir)));
        }
    }
}

