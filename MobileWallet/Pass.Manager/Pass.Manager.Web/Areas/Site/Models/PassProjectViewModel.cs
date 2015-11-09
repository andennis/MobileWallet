using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Pass.Manager.Web.Areas.Site.Validators;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Models
{
    [Validator(typeof(PassProjectViewModelValidator))]
    public class PassProjectViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Project"; } }
        public override int EntityId
        {
            get { return PassProjectId; }
        }

        public int PassProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PassTemplateId { get; set; }
        public int PassSiteId { get; set; }
        public int PassCertificateId { get; set; }
        public IEnumerable<SelectListItem> PassCertificates { get; set; }
    }
}