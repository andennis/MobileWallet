using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Common.Utils.Tests
{
    [TestFixture]
    public class CompressTests
    {
        private const string CompressData = @"Data\Compress";

        [Test]
        public void CompressUncompressDirectoryTest()
        {
            string resultFile = Path.Combine(CompressData, "TextFiles.zip");
            if (File.Exists(resultFile))
                File.Delete(resultFile);

            Assert.DoesNotThrow(() => Compress.CompressDirectory(CompressData, resultFile));
            Assert.True(File.Exists(resultFile));

            string resultDir = Path.Combine(CompressData, "TextFiles");
            if (Directory.Exists(resultDir))
                Directory.Delete(resultDir, true);

            Assert.DoesNotThrow(() => Compress.Uncompress(resultFile, resultDir));
            Assert.True(Directory.Exists(Path.Combine(resultDir, "Folder1")));
            Assert.True(File.Exists(Path.Combine(resultDir, @"Folder1\TextFile1.txt")));
            Assert.True(File.Exists(Path.Combine(resultDir, @"Folder1\TextFile2.txt")));
            Assert.True(Directory.Exists(Path.Combine(resultDir, @"Folder1\Folder2")));
            Assert.True(File.Exists(Path.Combine(resultDir, @"Folder1\Folder2\TextFile3.txt")));
            Assert.True(File.Exists(Path.Combine(resultDir, @"Folder1\Folder2\TextFile4.txt")));
        }

        [Test]
        public void CompressUncompressFilesTest()
        {
            string resultFile = Path.Combine(CompressData, "TextFiles.zip");
            if (File.Exists(resultFile))
                File.Delete(resultFile);

            var files = new List<string>()
                            {
                                Path.Combine(CompressData, @"Folder1\TextFile1.txt"),
                                Path.Combine(CompressData, @"Folder1\TextFile2.txt")
                            };

            Assert.DoesNotThrow(() => Compress.CompressFiles(files, resultFile));
            Assert.True(File.Exists(resultFile));

            string resultDir = Path.Combine(CompressData, "TextFiles");
            if (Directory.Exists(resultDir))
                Directory.Delete(resultDir, true);

            Assert.DoesNotThrow(() => Compress.Uncompress(resultFile, resultDir));
            Assert.True(File.Exists(Path.Combine(resultDir, "TextFile1.txt")));
            Assert.True(File.Exists(Path.Combine(resultDir, "TextFile2.txt")));
        }
    }
}
