using System.Collections.Generic;
using Ionic.Zip;

namespace Common.Utils
{
    public static class Compress
    {
        public static void CompressDirectory(string compressDirPath, string resultFilePath)
        {
            using (var zip = new ZipFile())
            {
                zip.AddDirectory(compressDirPath);
                zip.Save(resultFilePath);
            }
        }

        public static void CompressFiles(IEnumerable<string> files, string resultFilePath)
        {
            CompressFiles(files, string.Empty, resultFilePath);
        }

        public static void CompressFiles(IEnumerable<string> files, string dirPathInArchive, string resultFilePath)
        {
            using (var zip = new ZipFile())
            {
                zip.AddFiles(files, false, dirPathInArchive);
                zip.Save(resultFilePath);
            }
        }

        public static void Uncompress(string compressedFile, string resultDirPath)
        {
            using (ZipFile zip1 = ZipFile.Read(compressedFile))
            {
                foreach (ZipEntry zipItem in zip1)
                    zipItem.Extract(resultDirPath, ExtractExistingFileAction.OverwriteSilently);
            }
        }
    }
}
