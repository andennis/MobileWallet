using System.Text;
using Newtonsoft.Json;

namespace Pass.Container.Core.Entities.Templates.PassTemplate
{
    public class BarcodeDetails
    {
        [JsonProperty(PropertyName = "barcodeType", Required = Required.Always)]
        public BarcodeType BarcodeType { get; set; }

        [JsonProperty(PropertyName = "encodedMessage", Required = Required.Always)]
        public EncodedMessage EncodedMessage { get; set; }

        [JsonProperty(PropertyName = "textToEncode", NullValueHandling = NullValueHandling.Ignore)]
        public string TextToEncode { get; set; }

        [JsonProperty(PropertyName = "alternativeText", Required = Required.Always)]
        public AlternativeText AlternativeText { get; set; }

        [JsonProperty(PropertyName = "textToDisplay", NullValueHandling = NullValueHandling.Ignore)]
        public string TextToDisplay { get; set; }

        [JsonProperty(PropertyName = "encodingFormat", Required = Required.Always)]
        public Encoding EncodingFormat { get; set; }
    }

    public enum BarcodeType
    {
        Pdf417Code = 0,
        AztecCode = 1,
        QrCode = 2,
        DoNotDisplay = 3
    }

    public enum EncodedMessage
    {
        EncodeThePassSerialNumber = 0,
        EncodeThePassUniqueId = 1,
        EncodeTheUrlToUpdateThePass = 2,
        ProvideWhenPassIsCreated = 3,
        EncodeTheSameMessageOnEachPass = 4
    }

    public enum AlternativeText
    {
        DisplayTheBarcodeContent = 0,
        DisplayThePassSerialNumber = 1,
        DisplayThePassUniqueId = 2,
        ProvideWhenPassIsCreated = 3,
        DisplayTheSameMessageOnEachPass = 4,
        DoNotDisplayAnyAlternativeText = 5
    }
}
