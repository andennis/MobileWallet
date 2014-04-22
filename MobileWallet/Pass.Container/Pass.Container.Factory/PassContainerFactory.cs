﻿using CertificateStorage.Core;
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

        public static IPassTemplateService CreateTemplateService(IPassContainerConfig ptConfig, IFileStorageConfig fsConfig)
        {
            return _iocContainer.Resolve<IPassTemplateService>();
            //new DependencyOverride<IPassTemplateConfig>(ptConfig),
            //new DependencyOverride<IDbConfig>(ptConfig),
            //new DependencyOverride<IFileStorageConfig>(fsConfig));
        }

        public static IPassContainerService CreateContainerService(IDbConfig pdConfig)
        {
            return _iocContainer.Resolve<IPassContainerService>(/*new DependencyOverride<IDbConfig>(pdConfig)*/);
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

    }
}
