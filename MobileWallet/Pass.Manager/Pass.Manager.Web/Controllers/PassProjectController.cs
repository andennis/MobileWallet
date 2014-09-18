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
        public ActionResult CreateProject(string passSiteId)
        {
            return View("Create", new PassProjectViewModel() { PassSiteId = int.Parse(passSiteId) });
        }

        public static List<SelectListItem> GetPassProjectTypes()
        {
            var types = Enum.GetValues(typeof (PassProjectType)).Cast<PassProjectType>();
            List<SelectListItem> listItems = types.Select(type => new SelectListItem
                                                                  {
                                                                      Text = type.ToString(),
                                                                      Value = type.ToString()
                                                                  }).ToList();
            return listItems;
        }
    }
}