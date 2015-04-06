using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
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
        public PassProjectType ProjectType { get; set; }

        public int PassCertificateId { get; set; }
        public IEnumerable<SelectListItem> PassCertificates { get; set; }
    }
}