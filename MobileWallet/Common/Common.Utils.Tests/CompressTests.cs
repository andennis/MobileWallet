using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Common.Utils.Tests
{
    [TestFixture]
    public class CompressTests
    {
        private const string CompressData = @"Data\Compress\Folder1";
        [Test]
        public void CompressDirectoryTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CompressFilesTest()
        {
            string resultFile = Path.Combine(CompressData, "TextFiles.zip");
            if (File.Exists(resultFile))
                File.Delete(resultFile);

            var files = new List<string>()
                            {
                                Path.Combine(CompressData, "TextFile1.txt"),
                                Path.Combine(CompressData, "TextFile2.txt")
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
