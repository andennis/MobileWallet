using FluentValidation.Attributes;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(PassBeaconViewModelValidator))]
    public class PassBeaconViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Beacon"; } }
        public override int EntityId
        {
            get { return PassBeaconId; }
        }

        public int PassBeaconId { get; set; }
        public int? Major { get; set; }
        public int? Minor { get; set; }
        public string ProximityUuid { get; set; }
        public string RelevantText { get; set; }

        public int PassContentTemplateId { get; set; }
    }
}