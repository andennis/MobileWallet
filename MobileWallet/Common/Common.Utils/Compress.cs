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

        public static void Uncompress(string uncompressDirPath, string resultDirPath)
        {
            using (ZipFile zip1 = ZipFile.Read(uncompressDirPath))
            {
                foreach (ZipEntry zipItem in zip1)
                    zipItem.Extract(resultDirPath, ExtractExistingFileAction.OverwriteSilently);
            }
        }
    }
}
