using System.IO;

namespace Common.Utils
{
    public static class FileHelper
    {
        public static void DirectoryCopy(string sourceDir, string destDir, bool copySubDirs, bool overwrite = false)
        {
            var srcDirInfo = new DirectoryInfo(sourceDir);
            if (!srcDirInfo.Exists)
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDir);

            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);

            //Copy files
            foreach (FileInfo file in srcDirInfo.EnumerateFiles())
            {
                string dstPath = Path.Combine(destDir, file.Name);
                file.CopyTo(dstPath, overwrite);
            }

            if (!copySubDirs)
                return;

            //Copy subdirectories and their child directories and files
            foreach (DirectoryInfo subdir in srcDirInfo.EnumerateDirectories())
            {
                string dstPath = Path.Combine(destDir, subdir.Name);
                DirectoryCopy(subdir.FullName, dstPath, true, overwrite);
            }
        }

        public static string GetRandomFolderName()
        {
            string name = Path.GetRandomFileName();
            return name.Remove(name.Length - 4, 1);
        }

    }
}
