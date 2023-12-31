﻿using System;
using Common.Repository;
using FluentValidation.Attributes;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Areas.Site.Validators;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Models
{
    [Validator(typeof(PassContentTemplateViewModelValidator))]
    public class PassContentTemplateViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Content Template"; } }
        public override int EntityId
        {
            get { return PassContentTemplateId; }
        }

        public int PassContentTemplateId { get; set; }
        public EntityStatus Status { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OrganizationName { get; set; }

        public PassContentStyle PassStyle { get; set; }
        public PassTransitType? TransitType { get; set; }

        public PassBarcodeViewModel Barcode { get; set; }

        public int? MaxDistance { get; set; }
        public DateTime? RelevantDate { get; set; }

        //public ICollection<PassBeaconViewModel> Beacons { get; set; }
        //public ICollection<PassLocationViewModel> Locations { get; set; }

        public int? BackgroundColor { get; set; }
        public int? ForegroundColor { get; set; }
        public string GroupingIdentifier { get; set; }
        public int? LabelColor { get; set; }
        public string LogoText { get; set; }
        public bool SuppressStripShine { get; set; }

        public int PassProjectId { get; set; }
        public int? PassContainerTemplateId { get; set; }

        public bool IsOnline { get { return PassContainerTemplateId.HasValue; } }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}