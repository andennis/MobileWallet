using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.FieldDictionaryKeys;

namespace Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.LowerLevelKeys
{
    //Keys that define the structure of the pass
    public class PassStructure
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

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Transit
        {
            [EnumMember(Value = "PKTransitTypeAir")]
            PkTransitTypeAir = 0,
            [EnumMember(Value = "PKTransitTypeBoat")]
            PkTransitTypeBoat = 1,
            [EnumMember(Value = "PKTransitTypeBus")]
            PkTransitTypeBus = 2,
            [EnumMember(Value = "PKTransitTypeGeneric")]
            PkTransitTypeGeneric = 3,
            [EnumMember(Value = "PKTransitTypeTrain")]
            PkTransitTypeTrain = 4
        }
    }
}
