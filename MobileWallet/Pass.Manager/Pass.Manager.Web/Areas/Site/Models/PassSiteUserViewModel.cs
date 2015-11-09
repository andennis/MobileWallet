using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Areas.Site.Validators;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Areas.Site.Models
{
    [Validator(typeof(PassSiteUserViewModelValidator))]
    public class PassSiteUserViewModel : UserViewModel
    {
        public override int EntityId
        {
            get { return PassSiteUserId; }
        }

        public int PassSiteUserId { get; set; }
        public int PassSiteId { get; set; }
        public PassSiteUserState UserState { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}