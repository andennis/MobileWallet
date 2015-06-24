using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Common.Utils.Tests
{
    [TestFixture]
    public class FileHelperTest
    {
        private const string TestFolder = @"Data\FileHelper";
        private const string TestTempFolder = @"Data\FileHelper\Temp";

        [SetUp]
        public void InitAllTests()
        {
            if (Directory.Exists(TestTempFolder))
                Directory.Delete(TestTempFolder, true);

            Directory.CreateDirectory(TestTempFolder);
        }

        [Test]
        public void DirectoryCopyTest()
        {
            string srcDir = Path.Combine(TestFolder, "Dir1");

            Assert.DoesNotThrow(() => FileHelper.DirectoryCopy(srcDir, TestTempFolder, true));
            Assert.True(Directory.Exists(Path.Combine(TestTempFolder, "Dir11")));
            Assert.True(File.Exists(Path.Combine(TestTempFolder, "TextFile1.txt")));
            Assert.True(File.Exists(Path.Combine(TestTempFolder, "Dir11", "TextFile11.txt")));

            Assert.Throws<IOException>(() => FileHelper.DirectoryCopy(srcDir, TestTempFolder, true, false));
            Assert.DoesNotThrow(() => FileHelper.DirectoryCopy(srcDir, TestTempFolder, true, true));

            Directory.Delete(TestTempFolder, true);
            Directory.CreateDirectory(TestTempFolder);

            Assert.DoesNotThrow(() => FileHelper.DirectoryCopy(srcDir, TestTempFolder, false));
            Assert.False(Directory.Exists(Path.Combine(TestTempFolder, "Dir11")));
            Assert.True(File.Exists(Path.Combine(TestTempFolder, "TextFile1.txt")));
        }

        [Test]
        public void CopyFilesToDirectoryTest()
        {
            string dstPath = Path.Combine(TestTempFolder, "F1");
            string[] files1 =  Directory.GetFiles(Path.Combine(TestFolder, "Dir1"));
            FileHelper.CopyFilesToDirectory(files1, dstPath, true);

            string[] files2 = Directory.GetFiles(dstPath);
            CollectionAssert.AreEqual(files1.Select(Path.GetFileName), files2.Select(Path.GetFileName));
        }

        [Test]
        public void GetRandomFolderNameTest()
        {
            string name1 = FileHelper.GetRandomFolderName();
            string name2 = FileHelper.GetRandomFolderName();
            string name3 = FileHelper.GetRandomFolderName();
            Assert.AreNotEqual(name1, name2);
            Assert.AreNotEqual(name1, name3);
            Assert.AreNotEqual(name2, name3);
        }

        [Test]
        public void RenameTest()
        {
            string filePath = Path.Combine(TestTempFolder, "FileToReaname.txt");
            FileStream fs = File.Create(filePath);
            fs.Close();

            string filePath2 = FileHelper.Rename(filePath, "RenamedFile.txt");
            Assert.False(File.Exists(filePath));
            Assert.True(File.Exists(filePath2));
        }
    }
}
