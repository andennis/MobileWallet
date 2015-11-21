using System.Collections.Generic;
using System.Web.Mvc;
using Common.Web;
using Microsoft.Practices.Unity;

namespace Pass.Manager.Web.Common
{
    public class SiteFilterProvider : IFilterProvider
    {
        private readonly IUnityContainer _container;

        public SiteFilterProvider(IUnityContainer container)
        {
            _container = container;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            string areaName = controllerContext.RouteData.GetArea();
            if (string.IsNullOrEmpty(areaName) || areaName != "Site")
                return new Filter[0];

            var siteFilter = _container.Resolve<SiteFilterAttribute>();
            return new [] { new Filter(siteFilter, FilterScope.Global, null) };
        }
    }
}