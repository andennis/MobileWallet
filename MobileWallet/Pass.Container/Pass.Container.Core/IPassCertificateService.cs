using System;
using System.Security.Cryptography.X509Certificates;

namespace Pass.Container.Core
{
    public interface IPassCertificateService : IDisposable
    {
        X509Certificate2 GetCertificate(int certId);
    }
}