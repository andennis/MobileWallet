using FluentValidation.Attributes;
using Pass.Manager.Web.Areas.Site.Validators;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Areas.Site.Models
{
    [Validator(typeof(PassProjectFieldViewModelValidator))]
    public class PassProjectFieldViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Project Field"; } }
        public override int EntityId
        {
            get { return PassProjectFieldId; }
        }

        public int PassProjectId { get; set; }
        public int PassProjectFieldId { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public string DefaultLabel { get; set; }
        public string Description { get; set; }
    }
}