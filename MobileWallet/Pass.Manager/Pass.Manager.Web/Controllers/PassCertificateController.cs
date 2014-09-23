using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassCertificateController : BaseEntityController<PassCertificateViewModel, PassCertificate>
    {
        public PassCertificateController(IPassCertificateService certificateService)
            : base(certificateService)
        {
        }
    }
}