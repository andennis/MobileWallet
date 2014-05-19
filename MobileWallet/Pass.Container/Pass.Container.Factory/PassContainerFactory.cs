using System.Collections.Generic;
using CertificateStorage.Core;
using Common.Repository;
using FileStorage.Core;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Pass.Container.Core;

namespace Pass.Container.Factory
{
    public static class PassContainerFactory
    {
        private static readonly IUnityContainer _iocContainer = new UnityContainer();

        static PassContainerFactory()
        {
            _iocContainer.LoadConfiguration("FileStorage");
            _iocContainer.LoadConfiguration("PassContainer");
            _iocContainer.LoadConfiguration("CertificateStorage");
        }

        public static IPassTemplateService CreateTemplateService(ICertificateStorageService csService, IPassCertificateService pcService)
        {
            var ro = new List<ResolverOverride>();
            if (csService != null)
                ro.Add(new DependencyOverride<ICertificateStorageService>(csService));
            if (pcService != null)
                ro.Add(new DependencyOverride<IPassCertificateService>(pcService));
            
            return _iocContainer.Resolve<IPassTemplateService>(ro.ToArray());
            //new DependencyOverride<IPassTemplateConfig>(ptConfig),
            //new DependencyOverride<IDbConfig>(ptConfig),
            //new DependencyOverride<IFileStorageConfig>(fsConfig));
        }

        public static IPassService CreateContainerService(ICertificateStorageService csService, IPassCertificateService pcService)
        {
            var ro = new List<ResolverOverride>();
            if (csService != null)
                ro.Add(new DependencyOverride<ICertificateStorageService>(csService));
            if (pcService != null)
                ro.Add(new DependencyOverride<IPassCertificateService>(pcService));

            return _iocContainer.Resolve<IPassService>(ro.ToArray());
        }

        public static IApplePassProcessingService CreateApplePassProcessingService(IPassContainerConfig ptConfig, IFileStorageConfig fsConfig)
        {
            return _iocContainer.Resolve<IApplePassProcessingService>(
                new DependencyOverride<IPassContainerConfig>(ptConfig),
                new DependencyOverride<IDbConfig>(ptConfig),
                new DependencyOverride<IFileStorageConfig>(fsConfig));
        }

        public static ICertificateStorageService CreateCertificateStorageService()
        {
            return _iocContainer.Resolve<ICertificateStorageService>();
        }

        public static IPassCertificateService CreatePassCertificateService()
        {
            return _iocContainer.Resolve<IPassCertificateService>();
        }

        public static  IPassTemplateStorageService CreatePassTemplateStorageService()
        {
            return _iocContainer.Resolve<IPassTemplateStorageService>();
        }

    }
}
