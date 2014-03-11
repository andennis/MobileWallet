using Newtonsoft.Json;

namespace Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate.LowerLevelKeys
{
    //Information about a location
    public class Location
    {
        [JsonProperty(PropertyName = "altitude", NullValueHandling = NullValueHandling.Ignore)]
        public double? Altitude { get; set; }

        [JsonProperty(PropertyName = "latitude", Required = Required.Always)]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude", Required = Required.Always)]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "relevantText", NullValueHandling = NullValueHandling.Ignore)]
        public string RelevantText { get; set; }
    }
}
