using System.Xml.Serialization;
using Newtonsoft.Json;

namespace PromoSitePass.Models.GeneralPassTemplate
{
    public class BarcodeDetails
    {
        [XmlElement(ElementName = "barcodeType")]
        [JsonProperty(PropertyName = "barcodeType", Required = Required.Always)]
        public GeneralBarcodeType BarcodeType { get; set; }

        [XmlElement(ElementName = "encodedMessage")]
        [JsonProperty(PropertyName = "encodedMessage", Required = Required.Always)]
        public EncodedMessage EncodedMessage { get; set; }

        [XmlElement(ElementName = "textToEncode")]
        [JsonProperty(PropertyName = "textToEncode", NullValueHandling = NullValueHandling.Ignore)]
        public string TextToEncode { get; set; }

        [XmlElement(ElementName = "alternativeText")]
        [JsonProperty(PropertyName = "alternativeText", Required = Required.Always)]
        public AlternativeText AlternativeText { get; set; }

        [XmlElement(ElementName = "textToDisplay")]
        [JsonProperty(PropertyName = "textToDisplay", NullValueHandling = NullValueHandling.Ignore)]
        public string TextToDisplay { get; set; }

        [XmlElement(ElementName = "encodingFormat")]
        [JsonProperty(PropertyName = "encodingFormat", Required = Required.Always)]
        public string EncodingFormat { get; set; }
    }

    public enum GeneralBarcodeType
    {
        [XmlEnum(Name = "pdf417Code")]
        Pdf417Code = 0,
        [XmlEnum(Name = "aztecCode")]
        AztecCode = 1,
        [XmlEnum(Name = "qrCode")]
        QrCode = 2,
        [XmlEnum(Name = "doNotDisplay")]
        DoNotDisplay = 3
    }

    public enum EncodedMessage
    {
        [XmlEnum(Name = "encodeThePassSerialNumber")]
        EncodeThePassSerialNumber = 0,
        [XmlEnum(Name = "encodeThePassUniqueId")]
        EncodeThePassUniqueId = 1,
        [XmlEnum(Name = "encodeTheUrlToUpdateThePass")]
        EncodeTheUrlToUpdateThePass = 2,
        [XmlEnum(Name = "provideWhenPassIsCreated")]
        ProvideWhenPassIsCreated = 3,
        [XmlEnum(Name = "encodeTheSameMessageOnEachPass")]
        EncodeTheSameMessageOnEachPass = 4
    }

    public enum AlternativeText
    {
        DisplayTheBarcodeContent = 0,
        [XmlEnum(Name = "displayThePassSerialNumber")]
        DisplayThePassSerialNumber = 1,
        [XmlEnum(Name = "displayThePassUniqueId")]
        DisplayThePassUniqueId = 2,
        [XmlEnum(Name = "provideWhenPassIsCreated")]
        ProvideWhenPassIsCreated = 3,
        [XmlEnum(Name = "displayTheSameMessageOnEachPass")]
        DisplayTheSameMessageOnEachPass = 4,
        [XmlEnum(Name = "doNotDisplayAnyAlternativeText")]
        DoNotDisplayAnyAlternativeText = 5
    }
}
