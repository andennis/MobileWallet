using System;
using Newtonsoft.Json;

namespace Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.Lower_Level_Keys
{
    //Information about a location beacon
    public class Beacon
    {
        [JsonProperty(PropertyName = "major", NullValueHandling = NullValueHandling.Ignore)]
        public Int16? Major { get; set; }

        [JsonProperty(PropertyName = "minor", NullValueHandling = NullValueHandling.Ignore)]
        public Int16? Minor { get; set; }

        [JsonProperty(PropertyName = "proximityUUID", Required = Required.Always)]
        public string ProximityUuid { get; set; }

        [JsonProperty(PropertyName = "relevantText", NullValueHandling = NullValueHandling.Ignore)]
        public string RelevantText { get; set; }
    }
}