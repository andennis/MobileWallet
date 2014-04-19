using System;

namespace CertificateStorage.BL
{
    public class CertificateStorageException : Exception
    {
        public CertificateStorageException(string message)
            : base(message)
        {
        }
        public CertificateStorageException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
