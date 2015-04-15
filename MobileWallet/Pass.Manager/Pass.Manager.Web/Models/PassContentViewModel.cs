using FluentValidation.Attributes;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(PassContentViewModelValidator))]
    public class PassContentViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Content"; } }
        public override int EntityId
        {
            get { return PassContentId; }
        }

        public int PassContentId { get; set; }
        public int PassContentTemplateId { get; set; }
    }
}