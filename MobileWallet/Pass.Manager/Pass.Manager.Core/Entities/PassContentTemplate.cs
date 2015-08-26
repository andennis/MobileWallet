using System;
using System.Collections.Generic;
using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassContentTemplate : EntityVersionable
    {
        public int PassContentTemplateId { get; set; }
        public EntityStatus Status { get; set; }
        public bool IsDefault { get; set; }
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
        public ICollection<PassImage> PassImages { get; set; }

        public int? BackgroundColor { get; set; }
        public int? ForegroundColor { get; set; }
        public string GroupingIdentifier { get; set; }
        public int? LabelColor { get; set; }
        public string LogoText { get; set; }
        public bool SuppressStripShine { get; set; }

        public int PassProjectId { get; set; }
        public PassProject PassProject { get; set; }

        public ICollection<PassContentTemplateField> PassContentTemplateFields { get; set; }

        public int? PassContainerTemplateId { get; set; }
    }
}
