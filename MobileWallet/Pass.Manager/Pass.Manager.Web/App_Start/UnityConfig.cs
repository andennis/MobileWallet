using System;
using Microsoft.Practices.Unity;

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

        }
    }
}
