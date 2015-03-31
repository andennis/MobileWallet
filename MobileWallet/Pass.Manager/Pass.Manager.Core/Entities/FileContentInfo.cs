using System;
using System.IO;

namespace Pass.Manager.Core.Entities
{
    public class FileContentInfo : IDisposable
    {
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
    }
}
