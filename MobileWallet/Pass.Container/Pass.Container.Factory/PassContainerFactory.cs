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
        }

        public static IPassTemplateService Create(IPassTemplateConfig ptConfig, IFileStorageConfig fsConfig)
        {
            return _iocContainer.Resolve<IPassTemplateService>(new DependencyOverride<IPassTemplateConfig>(ptConfig),
                new DependencyOverride<IDbConfig>(ptConfig),
                new DependencyOverride<IFileStorageConfig>(fsConfig));
        }
    }
}
