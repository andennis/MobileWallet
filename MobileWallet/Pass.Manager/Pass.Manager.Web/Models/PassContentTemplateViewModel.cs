﻿using System;
using System.Collections.Generic;
using System.Drawing;
using FluentValidation.Attributes;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(PassContentTemplateViewModelValidator))]
    public class PassContentTemplateViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Content"; } }
        public override int EntityId
        {
            get { return PassContentTemplateId; }
        }

        public int PassContentTemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OrganizationName { get; set; }
        public PassContentStyle PassStyle { get; set; }

        #region Barcode
        /*
        public string BarcodeAltText { get; set; }
        public PassBarcodeFormat? BarcodeFormat { get; set; }
        public string BarcodeMessage { get; set; }
        public string BarcodeMessageEncoding { get; set; }
        */
        #endregion

        public int? MaxDistance { get; set; }
        public DateTime? RelevantDate { get; set; }

        public ICollection<PassBeacon> Beacons { get; set; }
        public ICollection<PassLocation> Locations { get; set; }

        public Color? BackgroundColor { get; set; }
        public Color? ForegroundColor { get; set; }
        public string GroupingIdentifier { get; set; }
        public Color? LabelColor { get; set; }
        public string LogoText { get; set; }
        public bool? SuppressStripShine { get; set; }

        public int PassProjectId { get; set; }

    }
}