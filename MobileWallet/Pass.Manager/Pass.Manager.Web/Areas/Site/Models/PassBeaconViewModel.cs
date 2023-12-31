﻿using FluentValidation.Attributes;
using Pass.Manager.Web.Areas.Site.Validators;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Models
{
    [Validator(typeof(PassBeaconViewModelValidator))]
    public class PassBeaconViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Beacon"; } }
        public override int EntityId
        {
            get { return PassBeaconId; }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int PassBeaconId { get; set; }
        public int? Major { get; set; }
        public int? Minor { get; set; }
        public string ProximityUuid { get; set; }
        public string RelevantText { get; set; }

        public int PassContentTemplateId { get; set; }
    }
}