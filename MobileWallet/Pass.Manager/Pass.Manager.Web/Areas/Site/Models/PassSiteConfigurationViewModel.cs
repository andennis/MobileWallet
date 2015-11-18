using FluentValidation.Attributes;
using Pass.Manager.Web.Areas.Site.Validators;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Models
{
    [Validator(typeof(PassSiteConfigurationViewModelValidator))]
    public class PassSiteConfigurationViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Site Configuration"; } }
        public override int EntityId
        {
            get { return PassSiteId; }
        }
        public int PassSiteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}