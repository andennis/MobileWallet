using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pass.Container.Core.Entities.Templates.PassTemplate
{
    public class FieldDetails
    {
        [JsonProperty(PropertyName = "auxiliaryFields", NullValueHandling = NullValueHandling.Ignore)]
        public List<Field> AuxiliaryFields { get; set; }

        [JsonProperty(PropertyName = "backFields", NullValueHandling = NullValueHandling.Ignore)]
        public List<Field> BackFields { get; set; }

        [JsonProperty(PropertyName = "headerFields", NullValueHandling = NullValueHandling.Ignore)]
        public List<Field> HeaderFields { get; set; }

        [JsonProperty(PropertyName = "primaryFields", NullValueHandling = NullValueHandling.Ignore)]
        public List<Field> PrimaryFields { get; set; }

        [JsonProperty(PropertyName = "secondaryFields", NullValueHandling = NullValueHandling.Ignore)]
        public List<Field> SecondaryFields { get; set; }

        //WARNING! Required for boarding passes; otherwise not allowed.
        [JsonProperty(PropertyName = "transitType", NullValueHandling = NullValueHandling.Ignore)]
        public Transit TransitType { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Transit
    {
        [EnumMember(Value = "Air")]
        Air = 0,
        [EnumMember(Value = "Boat")]
        Boat = 1,
        [EnumMember(Value = "Bus")]
        Bus = 2,
        [EnumMember(Value = "Generic")]
        Generic = 3,
        [EnumMember(Value = "Train")]
        Train = 4
    }

    public class Field
    {
        //Information about a field
        #region Standard Field Dictionary Keys

        //iOS 7.0
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
            [EnumMember(Value = "PhoneNumber")]
            PhoneNumber = 0,
            [EnumMember(Value = "Link")]
            Link = 1,
            [EnumMember(Value = "Address")]
            Address = 2,
            [EnumMember(Value = "CalendarEvent")]
            CalendarEvent = 3
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum TextAlignmentType
        {
            [EnumMember(Value = "Left")]
            Left = 0,
            [EnumMember(Value = "Center")]
            Center = 1,
            [EnumMember(Value = "Right")]
            Right = 2,
            [EnumMember(Value = "Natural")]
            Natural = 3
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum NumberStyleType
        {
            [EnumMember(Value = "Decimal")]
            Decimal = 0,
            [EnumMember(Value = "Percent")]
            Percent = 1,
            [EnumMember(Value = "Scientific")]
            Scientific = 2,
            [EnumMember(Value = "SpellOut")]
            SpellOut = 3
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum DateStyleType
        {
            [EnumMember(Value = "None")]
            None = 0,
            [EnumMember(Value = "Short")]
            Short = 1,
            [EnumMember(Value = "Medium")]
            Medium = 2,
            [EnumMember(Value = "Long")]
            Long = 3,
            [EnumMember(Value = "Full")]
            Full = 4
        }
    }
}
