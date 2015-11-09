using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Areas.Site.Models;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Controllers
{
    public class PassSiteCertificateController : BaseEntityController<PassSiteCertificateViewModel, PassSiteCertificate, PassSiteCertificateView, IPassSiteCertificateService, PassSiteCertificateFilter>
    {
        public PassSiteCertificateController(IPassSiteCertificateService siteCertificateService, IPassCertificateService certificateService)
            : base(siteCertificateService)
        {
        }
        
    }
}