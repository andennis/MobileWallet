using Pass.Manager.Core.Exceptions;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site
{
    public abstract class PmSiteWebViewPage<TModel> : PmWebViewPage<TModel>
    {
        public int SiteId
        {
            get
            {
                int siteId;
                if (!int.TryParse(ViewContext.RouteData.Values[SiteAreaRegistration.UrlPrmPassSiteId] as string, out siteId))
                    throw new PassManagerGeneralException(string.Format("URL does not contain the section '{0}'", SiteAreaRegistration.UrlPrmPassSiteId));

                return siteId;
            }
        }

    }
}