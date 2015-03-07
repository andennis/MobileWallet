using System;
using System.Collections.Generic;
using System.Drawing;
using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassContentTemplate : EntityVersionable
    {
        public int PassContentTemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OrganizationName { get; set; }

        public PassContentStyle PassStyle { get; set; }
        public PassTransitType? TransitType { get; set; }

        public PassBarcode Barcode { get; set; }
        
        public int? MaxDistance { get; set; }
        public DateTime? RelevantDate { get; set; }

        public ICollection<PassBeacon> Beacons { get; set; }
        public ICollection<PassLocation> Locations { get; set; }

        public Color? BackgroundColor { get; set; }
        public Color? ForegroundColor { get; set; }
        public string GroupingIdentifier { get; set; }
        public Color? LabelColor { get; set; }
        public string LogoText { get; set; }
        public bool SuppressStripShine { get; set; }

        public int PassProjectId { get; set; }
        public PassProject PassProject { get; set; }

        public ICollection<PassContentTemplateField> PassContentTemplateFields { get; set; }
    }
}
