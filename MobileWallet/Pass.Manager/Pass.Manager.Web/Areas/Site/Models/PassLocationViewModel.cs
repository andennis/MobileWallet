using FluentValidation.Attributes;
using Pass.Manager.Web.Areas.Site.Validators;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Models
{
    [Validator(typeof(PassLocationViewModelValidator))]
    public class PassLocationViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Location"; } }
        public override int EntityId
        {
            get { return PassLocationId; }
        }

        public int PassLocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Altitude { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string RelevantText { get; set; }

        public int PassContentTemplateId { get; set; }
    }
}