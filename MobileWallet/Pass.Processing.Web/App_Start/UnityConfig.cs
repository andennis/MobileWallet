using System.Web.Http;
using Common.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.WebApi;

namespace Pass.Processing.Web
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterInstance<ILogger>(NLogLogger.GetLoggingService());
            container.LoadConfiguration("FileStorage");
            container.LoadConfiguration("CertificateStorage");
            container.LoadConfiguration("PassContainer");

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}