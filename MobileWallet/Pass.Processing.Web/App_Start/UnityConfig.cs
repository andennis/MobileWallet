using System.Web.Http;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.WebApi;

namespace Pass.Processing.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.LoadConfiguration("FileStorage");
            container.LoadConfiguration("CertificateStorage");
            container.LoadConfiguration("PassContainer");
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}