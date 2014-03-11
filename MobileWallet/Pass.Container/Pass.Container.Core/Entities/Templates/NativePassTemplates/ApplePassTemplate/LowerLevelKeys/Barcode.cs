using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.LowerLevelKeys
{
    //Information about a pass’s barcode
    public class Barcode
    {
        [JsonProperty(PropertyName = "altText", NullValueHandling = NullValueHandling.Ignore)]
        public string AltText { get; set; }

        [JsonProperty(PropertyName = "format", Required = Required.Always)]
        public BarcodeType Format { get; set; }

        [JsonProperty(PropertyName = "message", Required = Required.Always)]
        public string Message { get; set; }

        //Text encoding that is used to convert the message
        //from the string representation to a data representation to
        //render the barcode. The value is typically iso-8859-1, but
        //you may use another encoding that is supported by your
        //barcode scanning infrastructure.
        [JsonProperty(PropertyName = "messageEncoding", Required = Required.Always)]
        public string MessageEncoding { get; set; }

        // http://msdn.microsoft.com/en-us/library/system.text.encodinginfo.name.aspx
    }

    public enum BarcodeType
    {
        [EnumMember(Value = "PKBarcodeFormatPDF417")]
        Pdf417Code = 0,
        [EnumMember(Value = "PKBarcodeFormatAztec")]
        AztecCode = 1,
        [EnumMember(Value = "PKBarcodeFormatQR")]
        QrCode = 2
    }
}
