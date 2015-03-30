using System;
using System.IO;

namespace FileStorage.Core.Entities
{
    public class StorageFileInfo : IDisposable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public long Size { get; set; }
        public Stream FileStream { get; set; }

        public void Dispose()
        {
            if (FileStream != null)
            {
                FileStream.Dispose();
                FileStream = null;
            }
        }
    }
}
