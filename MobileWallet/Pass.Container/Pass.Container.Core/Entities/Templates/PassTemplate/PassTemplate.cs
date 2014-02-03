using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Pass.Container.Core.Entities.Templates.PassTemplate
{
    [Serializable]
    [XmlRoot(ElementName = "passTemplate", Namespace = "http://www.mobilewallet.com")]
    public class PassTemplate
    {
        private const string Namespace = "http://www.mobilewallet.com";

        #region Standart keys

        [XmlElement(ElementName = "organizationName")]
        [JsonProperty(PropertyName = "organizationName", Required = Required.Always)]
        public string OrganizationName { get; set; }

        [XmlElement(ElementName = "templateName")]
        [JsonProperty(PropertyName = "templateName", Required = Required.Always)]
        public string TemplateName { get; set; }

        [XmlElement(ElementName = "templateDescription")]
        [JsonProperty(PropertyName = "templateDescription")]
        public string TemplateDescription { get; set; }

        [XmlElement(ElementName = "passType")]
        [JsonProperty(PropertyName = "passType", Required = Required.Always)]
        public PassType PassType { get; set; }

        [XmlElement(ElementName = "passDescription")]
        [JsonProperty(PropertyName = "passDescription", Required = Required.Always)]
        public string PassDescription { get; set; }

        [XmlElement(ElementName = "passSerialNumberType")]
        [JsonProperty(PropertyName = "passSerialNumberType", Required = Required.Always)]
        public PassSerialNumberType PassSerialNumberType { get; set; }

        [XmlElement(ElementName = "passCertificate")]
        [JsonProperty(PropertyName = "passCertificate", Required = Required.Always)]
        public string PassCertificate { get; set; }

        #endregion

        #region Visual Appearance Keys

        [XmlElement(ElementName = "backgroundColor")]
        [JsonProperty(PropertyName = "backgroundColor", Required = Required.Always)]
        public Color BackgroundColor { get; set; }

        [XmlElement(ElementName = "labelTextColor")]
        [JsonProperty(PropertyName = "labelTextColor", Required = Required.Always)]
        public Color LabelTextColor { get; set; }

        [XmlElement(ElementName = "valueTextColor")]
        [JsonProperty(PropertyName = "valueTextColor", Required = Required.Always)]
        public Color ValueTextColor { get; set; }

        [XmlElement(ElementName = "suppressStripShine")]
        [JsonProperty(PropertyName = "suppressStripShine", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SuppressStripShine { get; set; }

        #endregion

        #region Others Keys

        //IOS 7
        //WARNING! Optional for event tickets and boarding passes; otherwise not allowed
        [XmlElement(ElementName = "groupingIdentifier")]
        [JsonProperty(PropertyName = "groupingIdentifier")]
        public string GroupingIdentifier { get; set; }

        [XmlElement(ElementName = "passTimezone")]
        [JsonProperty(PropertyName = "passTimezone")]
        public TimeZone PassTimezone { get; set; }

        [XmlElement(ElementName = "logoText")]
        [JsonProperty(PropertyName = "logoText", NullValueHandling = NullValueHandling.Ignore)]
        public string LogoText { get; set; }

        #endregion

        #region Integration Details

        [XmlElement(ElementName = "integrationDetails")]
        [JsonProperty(PropertyName = "integrationDetails")]
        public IntegrationDetails IntegrationDetails { get; set; }

        #endregion

        #region Location Details

        [XmlElement(ElementName = "locationDetails")]
        [JsonProperty(PropertyName = "locationDetails", NullValueHandling = NullValueHandling.Ignore)]
        public LocationDetails LocationDetails { get; set; }

        #endregion

        #region Beacon Details

        [XmlElement(ElementName = "beaconDetails")]
        [JsonProperty(PropertyName = "beaconDetails", NullValueHandling = NullValueHandling.Ignore)]
        public BeaconDetails BeaconDetails { get; set; }

        #endregion

        #region Distribution Details

        [XmlElement(ElementName = "distributionDetails")]
        [JsonProperty(PropertyName = "distributionDetails", Required = Required.Always)]
        public DistributionDetails DistributionDetails { get; set; }

        #endregion

        #region Barcode Details

        [XmlElement(ElementName = "barcodeDetails")]
        [JsonProperty(PropertyName = "barcodeDetails", Required = Required.Always)]
        public BarcodeDetails BarcodeDetails { get; set; }

        #endregion

        #region Field Details

        [XmlElement(ElementName = "fieldDetails")]
        [JsonProperty(PropertyName = "fieldDetails", Required = Required.Always)]
        public FieldDetails FieldDetails { get; set; }

        #endregion

        //???????????????
        //#region Language Details 

        //[XmlElement(ElementName = "languageDetails")]
        //[JsonProperty(PropertyName = "languageDetails")]
        //public LanguageDetails LanguageDetails { get; set; }

        //#endregion

        public void SaveToXml(string filePath)
        {
            var xws = new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 };
            var serializer = new XmlSerializer(this.GetType());
            using (var xmlWriter = XmlWriter.Create(filePath, xws))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add("", Namespace);
                serializer.Serialize(xmlWriter, this, ns);
            }
        }

        public PassTemplate LoadStreamXml(Stream fileStream)
        {
            var doc = new XmlDocument();
            doc.Load(fileStream);

            using (var read = new StringReader(doc.OuterXml))
            using (var reader = new XmlTextReader(read))
            {
                var serializer = new XmlSerializer(this.GetType(), Namespace);
                return (PassTemplate)serializer.Deserialize(reader);
            }
        }

        public PassTemplate LoadFromXml(string fileName)
        {
            var doc = new XmlDocument();
            doc.Load(fileName);

            using (var read = new StringReader(doc.OuterXml))
            using (var reader = new XmlTextReader(read))
            {
                var serializer = new XmlSerializer(this.GetType(), Namespace);
                return (PassTemplate)serializer.Deserialize(reader);
            }
        }
    }

    public enum PassType
    {
        [XmlEnum(Name = "boardingPass")]
        BoardingPass = 0,
        [XmlEnum(Name = "coupon")]
        Coupon = 1,
        [XmlEnum(Name = "eventTicket")]
        EventTicket = 2,
        [XmlEnum(Name = "generic")]
        Generic = 3,
        [XmlEnum(Name = "storeCard")]
        StoreCard = 4
    }

    public enum PassSerialNumberType
    {
        [XmlEnum(Name = "autoGgenerated")]
        AutoGgenerated = 0,
        [XmlEnum(Name = "provided")]
        Provided = 1,
        [XmlEnum(Name = "sameForEachPass")]
        SameForEachPass = 2
    }
}
