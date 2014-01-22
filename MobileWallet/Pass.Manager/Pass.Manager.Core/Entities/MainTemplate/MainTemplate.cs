using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;


namespace Pass.Manager.Core.Entities.MainTemplate
{
    public class MainTemplate
    {
        #region Standart keys

        [JsonProperty(PropertyName = "organizationName", Required = Required.Always)]
        public string OrganizationName { get; set; }

        [JsonProperty(PropertyName = "templateName", Required = Required.Always)]
        public string TemplateName { get; set; }

        [JsonProperty(PropertyName = "templateDescription")]
        public string TemplateDescription { get; set; }

        [JsonProperty(PropertyName = "passType", Required = Required.Always)]
        public PassType PassType { get; set; }

        [JsonProperty(PropertyName = "passDescription", Required = Required.Always)]
        public string PassDescription { get; set; }

        [JsonProperty(PropertyName = "passSerialNumberType", Required = Required.Always)]
        public PassSerialNumberType PassSerialNumberType { get; set; }

        [JsonProperty(PropertyName = "passCertificate", Required = Required.Always)]
        public string PassCertificate { get; set; }

        #endregion
        
        #region Visual Appearance Keys

        [JsonProperty(PropertyName = "backgroundColor", NullValueHandling = NullValueHandling.Ignore)]
        public Color BackgroundColor { get; set; }

        [JsonProperty(PropertyName = "labelTextColor", NullValueHandling = NullValueHandling.Ignore)]
        public Color LabelTextColor { get; set; }

        [JsonProperty(PropertyName = "valueTextColor", NullValueHandling = NullValueHandling.Ignore)]
        public Color ValueTextColor { get; set; }

        [JsonProperty(PropertyName = "suppressStripShine", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SuppressStripShine { get; set; }

        #endregion

        #region Integration Details

        [JsonProperty(PropertyName = "integrationDetails")]
        public IntegrationDetails IntegrationDetails { get; set; }

        #endregion
        
        #region Location Details

        [JsonProperty(PropertyName = "locationDetails", NullValueHandling = NullValueHandling.Ignore)]
        public LocationDetails LocationDetails { get; set; }

        #endregion
        
        #region Beacon Details

        [JsonProperty(PropertyName = "beaconDetails", NullValueHandling = NullValueHandling.Ignore)]
        public BeaconDetails BeaconDetails { get; set; }

        #endregion
       
        #region Distribution Details

        [JsonProperty(PropertyName = "distributionDetails", Required = Required.Always)]
        public DistributionDetails DistributionDetails { get; set; }

        #endregion
       
        #region Barcode Details

        [JsonProperty(PropertyName = "barcodeDetails", Required = Required.Always)]
        public BarcodeDetails BarcodeDetails { get; set; }

        #endregion

        //IOS 7
        //WARNING! Optional for event tickets and boarding passes; otherwise not allowed
        [JsonProperty(PropertyName = "groupingIdentifier", Required = Required.Always)]
        public string GroupingIdentifier { get; set; }

        [JsonProperty(PropertyName = "passTimezone", Required = Required.Always)]
        public TimeZone PassTimezone { get; set; }

        [JsonProperty(PropertyName = "logoText", NullValueHandling = NullValueHandling.Ignore)]
        public string LogoText { get; set; }
    }

    public enum PassType
    {
        BoardingPass = 0,
        Coupon = 1,
        EventTicket = 2,
        Generic = 3,
        StoreCard = 4
    }

    public enum PassSerialNumberType
    {
        AutoGgenerated = 0,
        Provided = 1,
        SameForEachPass = 2
    }
}
