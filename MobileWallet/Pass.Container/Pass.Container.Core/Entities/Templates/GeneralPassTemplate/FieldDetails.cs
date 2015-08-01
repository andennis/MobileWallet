using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pass.Container.Core.Entities.Templates.GeneralPassTemplate
{
    public class FieldDetails
    {
        [XmlArray(ElementName = "auxiliaryFields", IsNullable = false)]
        [XmlArrayItem(ElementName = "field", IsNullable = false)]
        [JsonProperty(PropertyName = "auxiliaryFields", NullValueHandling = NullValueHandling.Ignore)]
        public List<GeneralField> AuxiliaryFields { get; set; }

        [XmlArray(ElementName = "backFields", IsNullable = false)]
        [XmlArrayItem(ElementName = "field", IsNullable = false)]
        [JsonProperty(PropertyName = "backFields", NullValueHandling = NullValueHandling.Ignore)]
        public List<GeneralField> BackFields { get; set; }

        [XmlArray(ElementName = "headerFields", IsNullable = false)]
        [XmlArrayItem(ElementName = "field", IsNullable = false)]
        [JsonProperty(PropertyName = "headerFields", NullValueHandling = NullValueHandling.Ignore)]
        public List<GeneralField> HeaderFields { get; set; }

        [XmlArray(ElementName = "primaryFields", IsNullable = false)]
        [XmlArrayItem(ElementName = "field", IsNullable = false)]
        [JsonProperty(PropertyName = "primaryFields", NullValueHandling = NullValueHandling.Ignore)]
        public List<GeneralField> PrimaryFields { get; set; }

        [XmlArray(ElementName = "secondaryFields", IsNullable = false)]
        [XmlArrayItem(ElementName = "field", IsNullable = false)]
        [JsonProperty(PropertyName = "secondaryFields", NullValueHandling = NullValueHandling.Ignore)]
        public List<GeneralField> SecondaryFields { get; set; }

        //WARNING! Required for boarding passes; otherwise not allowed.
        [XmlElement(ElementName = "transitType")]
        [JsonProperty(PropertyName = "transitType", NullValueHandling = NullValueHandling.Ignore)]
        public Transit? TransitType { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Transit
    {
        [XmlEnum(Name = "air")]
        [EnumMember(Value = "air")]
        Air = 0,

        [XmlEnum(Name = "boat")]
        [EnumMember(Value = "boat")]
        Boat = 1,

        [XmlEnum(Name = "bus")]
        [EnumMember(Value = "bus")]
        Bus = 2,

        [XmlEnum(Name = "generic")]
        [EnumMember(Value = "generic")]
        Generic = 3,

        [XmlEnum(Name = "train")]
        [EnumMember(Value = "train")]
        Train = 4
    }

    public class GeneralField
    {
        //Information about a field
        #region Standard Field Dictionary Keys

        //iOS 7.0 example, <a href = "http://example.com/customers/123"> Edit my profile </a> is displayed as a link with the text “Edit my profile”.
        [XmlElement(ElementName = "attributedValue", IsNullable = false)]
        [JsonProperty(PropertyName = "attributedValue", NullValueHandling = NullValueHandling.Ignore)]
        public string AttributedValue { get; set; }

        [XmlElement(ElementName = "changeMessage", IsNullable = false)]
        [JsonProperty(PropertyName = "changeMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string ChangeMessage { get; set; }

        [XmlArray(ElementName = "dataDetectorTypes", IsNullable = false)]
        [XmlArrayItem(ElementName = "dataDetector", IsNullable = false)]
        [JsonProperty(PropertyName = "dataDetectorTypes", NullValueHandling = NullValueHandling.Ignore)]
        public List<DataDetector> DataDetectorTypes { get; set; }

        [XmlElement(ElementName = "key")]
        [JsonProperty(PropertyName = "key", Required = Required.Always)]
        public string Key { get; set; }

        [XmlElement(ElementName = "label", IsNullable = false)]
        [JsonProperty(PropertyName = "label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [XmlElement(ElementName = "isDynamicLabel")]
        [JsonProperty(PropertyName = "isDynamicLabel", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDynamicLabel { get; set; }

        [XmlElement(ElementName = "value")]
        [JsonProperty(PropertyName = "value", Required = Required.Always)]
        public string Value { get; set; }

        [XmlElement(ElementName = "isDynamicValue")]
        [JsonProperty(PropertyName = "isDynamicValue", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDynamicValue { get; set; }

        [XmlElement(ElementName = "textAlignment")]
        [JsonProperty(PropertyName = "textAlignment", NullValueHandling = NullValueHandling.Ignore)]
        public TextAlignmentType? TextAlignment { get; set; }

        [XmlElement(ElementName = "type")]
        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        public DataType? FieldType { get; set; }

        #endregion

        //Information about how a number should be displayed in a field
        #region Number Style Keys
        [XmlElement(ElementName = "numberStyle")]
        [JsonProperty(PropertyName = "numberStyle", NullValueHandling = NullValueHandling.Ignore)]
        public NumberStyleType? NumberStyle { get; set; }

        [XmlElement(ElementName = "currencyCode", IsNullable = false)]
        [JsonProperty(PropertyName = "currencyCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrencyCode { get; set; }

        #endregion

        //Information about how a date should be displayed in a field
        #region Date Style Keys

        [XmlElement(ElementName = "dateStyle")]
        [JsonProperty(PropertyName = "dateStyle", NullValueHandling = NullValueHandling.Ignore)]
        public DateStyleType? DateStyle { get; set; }

        [XmlElement(ElementName = "isRelative")]
        [JsonProperty(PropertyName = "isRelative", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsRelative { get; set; }

        [XmlElement(ElementName = "timeStyle")]
        [JsonProperty(PropertyName = "timeStyle", NullValueHandling = NullValueHandling.Ignore)]
        public DateStyleType? TimeStyle { get; set; }

        [XmlElement(ElementName = "ignoresTimeZone")]
        [JsonProperty(PropertyName = "ignoresTimeZone", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IgnoresTimeZone { get; set; }

        #endregion

        [JsonConverter(typeof(StringEnumConverter))]
        public enum DataType
        {
            [XmlEnum(Name = "text")]
            [EnumMember(Value = "text")]
            Text = 0,

            [XmlEnum(Name = "number")]
            [EnumMember(Value = "number")]
            Number = 1,

            [XmlEnum(Name = "currency")]
            [EnumMember(Value = "currency")]
            Currency = 2,

            [XmlEnum(Name = "date")]
            [EnumMember(Value = "date")]
            Date = 3,

            [XmlEnum(Name = "dateTime")]
            [EnumMember(Value = "dateTime")]
            DateTime = 3
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum DataDetector
        {
            [XmlEnum(Name = "phoneNumber")]
            [EnumMember(Value = "phoneNumber")]
            PhoneNumber = 0,

            [XmlEnum(Name = "link")]
            [EnumMember(Value = "link")]
            Link = 1,

            [XmlEnum(Name = "address")]
            [EnumMember(Value = "address")]
            Address = 2,

            [XmlEnum(Name = "calendarEvent")]
            [EnumMember(Value = "calendarEvent")]
            CalendarEvent = 3
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum TextAlignmentType
        {
            [XmlEnum(Name = "left")]
            [EnumMember(Value = "left")]
            Left = 0,

            [XmlEnum(Name = "center")]
            [EnumMember(Value = "center")]
            Center = 1,

            [XmlEnum(Name = "right")]
            [EnumMember(Value = "right")]
            Right = 2,

            [XmlEnum(Name = "natural")]
            [EnumMember(Value = "natural")]
            Natural = 3
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum NumberStyleType
        {
            [XmlEnum(Name = "decimal")]
            [EnumMember(Value = "decimal")]
            Decimal = 0,

            [XmlEnum(Name = "percent")]
            [EnumMember(Value = "percent")]
            Percent = 1,

            [XmlEnum(Name = "scientific")]
            [EnumMember(Value = "scientific")]
            Scientific = 2,

            [XmlEnum(Name = "spellOut")]
            [EnumMember(Value = "spellOut")]
            SpellOut = 3
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum DateStyleType
        {
            [XmlEnum(Name = "none")]
            [EnumMember(Value = "none")]
            None = 0,

            [XmlEnum(Name = "short")]
            [EnumMember(Value = "short")]
            Short = 1,

            [XmlEnum(Name = "medium")]
            [EnumMember(Value = "medium")]
            Medium = 2,

            [XmlEnum(Name = "long")]
            [EnumMember(Value = "long")]
            Long = 3,

            [XmlEnum(Name = "full")]
            [EnumMember(Value = "full")]
            Full = 4
        }
    }
}
