using System.IO;

namespace Common.Utils
{
    public static class FileHelper
    {
        public static void DirectoryCopy(string sourceDir, string destDir, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            var srcDirInfo = new DirectoryInfo(sourceDir);

            if (!srcDirInfo.Exists)
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDir);

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = srcDirInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDir, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                DirectoryInfo[] dirs = srcDirInfo.GetDirectories();
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDir, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, true);
                }
            }
        }

        public static string GetRandomFolderName()
        {
            string name = Path.GetRandomFileName();
            return name.Remove(name.Length - 4, 1);
        }

    }
}
