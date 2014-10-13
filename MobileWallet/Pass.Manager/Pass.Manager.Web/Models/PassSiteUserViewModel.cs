using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
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