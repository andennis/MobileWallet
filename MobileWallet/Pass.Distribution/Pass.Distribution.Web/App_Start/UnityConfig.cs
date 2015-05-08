using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Pass.Distribution.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
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
        }
    }
}
