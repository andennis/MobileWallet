using System;
using System.IO;
using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassImage : EntityVersionable, IDisposable
    {
        public int PassImageId { get; set; }
        public PassImageType ImageType { get; set; }
        public int? FileStorageId { get; set; }
        public int? FileStorage2xId { get; set; }

        public int PassContentTemplateId { get; set; }
        public PassContentTemplate PassContentTemplate { get; set; }

        public FileContentInfo ImageFile { get; set; }
        public FileContentInfo ImageFile2x { get; set; }

        public void Dispose()
        {
            if (ImageFile != null)
            {
                ImageFile.Dispose();
                ImageFile = null;
            }
            if (ImageFile2x != null)
            {
                ImageFile2x.Dispose();
                ImageFile2x = null;
            }
        }
    }
}
