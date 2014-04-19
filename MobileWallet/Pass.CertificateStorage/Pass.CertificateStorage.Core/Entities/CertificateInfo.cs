﻿using System;
using System.IO;
using System.Security;

namespace Pass.CertificateStorage.Core.Entities
{
    public class CertificateInfo : IDisposable
    {
        public string Name { get; set; }
        public Stream CertificateFile { get; set; }
        public SecureString Password { get; set; }

        #region IDisposable
        public void Dispose()
        {
            if (CertificateFile != null)
            {
                CertificateFile.Dispose();
                CertificateFile = null;
            }

            if (Password != null)
            {
                Password.Dispose();
                Password = null;
            }
        }
        #endregion
    }
}
