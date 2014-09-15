using FluentValidation.Attributes;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(PassSiteViewModelValidator))]
    public class PassSiteViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Site"; } }
        public int PassSiteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override int EntityId
        {
            get { return PassSiteId; }
        }
    }
}