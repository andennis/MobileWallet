using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Field_Dictionary_Keys
{
    public class Field
    {
        //Information about a field
        #region Standard Field Dictionary Keys

        //Available in iOS 7.0
        [JsonProperty(PropertyName = "attributedValue", NullValueHandling = NullValueHandling.Ignore)]
        public string AttributedValue { get; set; }

        [JsonProperty(PropertyName = "changeMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string ChangeMessage { get; set; }

        [JsonProperty(PropertyName = "dataDetectorTypes", NullValueHandling = NullValueHandling.Ignore)]
        public List<DataDetector> DataDetectorTypes { get; set; }

        [JsonProperty(PropertyName = "key", Required = Required.Always)]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "value", Required = Required.Always)]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "textAlignment", NullValueHandling = NullValueHandling.Ignore)]
        public TextAlignmentType TextAlignment { get; set; }

        #endregion

        //Information about how a number should be displayed in a field
        #region Number Style Keys
        [JsonProperty(PropertyName = "numberStyle", NullValueHandling = NullValueHandling.Ignore)]
        public NumberStyleType NumberStyle { get; set; }

        [JsonProperty(PropertyName = "currencyCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrencyCode { get; set; }

        #endregion

        //Information about how a date should be displayed in a field
        #region Date Style Keys
        [JsonProperty(PropertyName = "dateStyle", NullValueHandling = NullValueHandling.Ignore)]
        public DateStyleType DateStyle { get; set; }

        [JsonProperty(PropertyName = "ignoresTimeZone", NullValueHandling = NullValueHandling.Ignore)]
        public bool IgnoresTimeZone { get; set; }

        [JsonProperty(PropertyName = "isRelative", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsRelative { get; set; }

        [JsonProperty(PropertyName = "timeStyle", NullValueHandling = NullValueHandling.Ignore)]
        public DateStyleType TimeStyle { get; set; }

        #endregion

        [JsonConverter(typeof(StringEnumConverter))]
        public enum DataDetector
        {
            [EnumMember(Value = "PKDataDetectorTypePhoneNumber")]
            PkDataDetectorTypePhoneNumber = 0,
            [EnumMember(Value = "PKDataDetectorTypeLink")]
            PkDataDetectorTypeLink = 1,
            [EnumMember(Value = "PKDataDetectorTypeAddress")]
            PkDataDetectorTypeAddress = 2,
            [EnumMember(Value = "PKDataDetectorTypeCalendarEvent")]
            PkDataDetectorTypeCalendarEvent = 3
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum TextAlignmentType
        {
            [EnumMember(Value = "PKTextAlignmentLeft")]
            PkTextAlignmentLeft = 0,
            [EnumMember(Value = "PKTextAlignmentCenter")]
            PkTextAlignmentCenter = 1,
            [EnumMember(Value = "PKTextAlignmentRight")]
            PkTextAlignmentRight = 2,
            [EnumMember(Value = "PKTextAlignmentNatural")]
            PkTextAlignmentNatural = 3
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum NumberStyleType
        {
            [EnumMember(Value = "PKNumberStyleDecimal")]
            PkNumberStyleDecimal = 0,
            [EnumMember(Value = "PKNumberStylePercent")]
            PkNumberStylePercent = 1,
            [EnumMember(Value = "PKNumberStyleScientific")]
            PkNumberStyleScientific = 2,
            [EnumMember(Value = "PKNumberStyleSpellOut")]
            PkNumberStyleSpellOut = 3
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum DateStyleType
        {
            [EnumMember(Value = "PKDateStyleNone")]
            PkDateStyleNone = 0,
            [EnumMember(Value = "PKDateStyleShort")]
            PkDateStyleShort = 1,
            [EnumMember(Value = "PKDateStyleMedium")]
            PkDateStyleMedium = 2,
            [EnumMember(Value = "PKDateStyleLong")]
            PkDateStyleLong = 3,
            [EnumMember(Value = "PKDateStyleFull")]
            PkDateStyleFull = 4
        }
    }
}
