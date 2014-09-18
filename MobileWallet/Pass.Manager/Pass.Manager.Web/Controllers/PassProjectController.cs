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
    public class PassProjectController : BaseEntityController<PassProjectViewModel, PassProject>
    {
        public PassProjectController(IPassProjectService passService)
            : base(passService)
        {
        }

        [HttpGet]
        public ActionResult CreateProject(int passSiteId)
        {
            return View("Create", new PassProjectViewModel() { PassSiteId = passSiteId });
        }

        public static IEnumerable<SelectListItem> GetPassProjectTypes()
        {
            var types = Enum.GetValues(typeof (PassProjectType)).Cast<PassProjectType>();
            IEnumerable<SelectListItem> listItems = types.Select(type => new SelectListItem
                                                                  {
                                                                      Text = type.ToString(),
                                                                      Value = ((int)type).ToString()
                                                                  });
            return listItems;
        }
    }
}