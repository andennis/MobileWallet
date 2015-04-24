using System;
using System.Security;
using Common.Utils;

namespace CertificateStorage.Core.Entities
{
    public class CertificateInfo : IDisposable
    {
        public int CertificateId { get; set; }
        public string Name { get; set; }
        public FileContentInfo CertificateFile { get; set; }
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
