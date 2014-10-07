using System;
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
            container.LoadConfiguration("FileStorage");
            container.LoadConfiguration("CertificateStorage");
            container.LoadConfiguration("PassManager");
        }
    }
}
