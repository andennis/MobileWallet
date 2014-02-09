using Common.Repository;
using FileStorage.Core;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace FileStorage.Factory
{
    public static class FileStorageFactory
    {
        private static readonly IUnityContainer _iocContainer = new UnityContainer();
        
        static FileStorageFactory()
        {
            _iocContainer.LoadConfiguration("FileStorage");
        }

        public static IFileStorageService Create(IFileStorageConfig config = null)
        {
            if (config == null)
                return _iocContainer.Resolve<IFileStorageService>();

            return _iocContainer.Resolve<IFileStorageService>(new DependencyOverride<IFileStorageConfig>(config), 
                new DependencyOverride<IDbConfig>(config));
        }
    }
}
