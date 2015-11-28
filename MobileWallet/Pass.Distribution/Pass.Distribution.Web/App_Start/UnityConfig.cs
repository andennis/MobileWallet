using System;
using Common.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Pass.Distribution.Web
{
    public class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return _container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.LoadConfiguration("FileStorage");
            container.LoadConfiguration("CertificateStorage");
            container.LoadConfiguration("PassContainer");
            container.LoadConfiguration("PassManager");
            container.LoadConfiguration("PassDistribution");

            container.RegisterInstance<ILogger>(NLogLogger.GetLoggingService());
        }
    }
}
