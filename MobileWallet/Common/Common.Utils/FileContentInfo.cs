using System;
using System.IO;

namespace Common.Utils
{
    public class FileContentInfo : IDisposable, ICloneable
    {
        /*
        public FileContentInfo()
        {
        }

        public FileContentInfo(Stream stream)
        {
            var fs = stream as FileStream;
            if (fs != null)
            {
                FileName = fs.Name;
                ContentStream = fs;
                return;
            }
            
        }
        */

        public string FileName { get; set; }
        public Stream ContentStream { get; set; }
        public string ContentType { get; set; }

        public void Dispose()
        {
            if (ContentStream != null)
            {
                ContentStream.Dispose();
                ContentStream = null;
            }
        }
        public object Clone()
        {
            return new FileContentInfo()
                   {
                       FileName = this.FileName,
                       ContentType = this.ContentType,
                       ContentStream = this.ContentStream
                   };
        }
    }
}
