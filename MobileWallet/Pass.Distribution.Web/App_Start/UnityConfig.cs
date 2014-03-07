using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Mvc5;

namespace Pass.Distribution.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.LoadConfiguration("FileStorage");
            container.LoadConfiguration("PassContainer");
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}