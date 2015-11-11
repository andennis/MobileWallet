using System;
using Common.Logging;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Pass.Manager.Web
{
    public class UnityConfig
    {
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

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance<ILogger>(NLogLogger.GetLoggingService());
            container.LoadConfiguration("FileStorage");
            container.LoadConfiguration("CertificateStorage");
            container.LoadConfiguration("PassContainer");
            container.LoadConfiguration("PassManager");
            container.LoadConfiguration("PassDistribution");
            container.LoadConfiguration("PassNotification");
            container.LoadConfiguration("PassManager.Web");

            container.RegisterType<HttpContextBase>(new InjectionFactory(_ => new HttpContextWrapper(HttpContext.Current)));
        }
    }
}
