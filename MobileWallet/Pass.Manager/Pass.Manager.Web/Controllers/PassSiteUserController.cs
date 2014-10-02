using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassSiteUserController : BaseEntityController<PassSiteUserViewModel, PassSiteUser, IPassSiteUserService>
    {
        public PassSiteUserController(IPassSiteUserService siteUserService)
            : base(siteUserService)
        {
            
        }

    }
}