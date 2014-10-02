using System;
using System.Web.Mvc;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassCertificateController : BaseEntityController<PassCertificateAppleViewModel, PassCertificateApple, IPassCertificateService, SearchFilterBase>
    {
        public PassCertificateController(IPassCertificateService certificateService)
            : base(certificateService)
        {
        }

        public override ActionResult Create()
        {
            //TODO temp solution to specify ExpDate
            return View(new PassCertificateAppleViewModel(){ExpDate = DateTime.Today});
        }
    }
}