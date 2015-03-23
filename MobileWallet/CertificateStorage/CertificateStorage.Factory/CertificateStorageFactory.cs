using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertificateStorage.Core;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace CertificateStorage.Factory
{
    public static class CertificateStorageFactory
    {
        private static readonly IUnityContainer _iocContainer = new UnityContainer();

        static CertificateStorageFactory()
        {
            _iocContainer.LoadConfiguration("CertificateStorage");
        }

        public static ICertificateStorageService Create(ICertificateStorageConfig config = null)
        {
           return _iocContainer.Resolve<ICertificateStorageService>();
        }
    }
}
