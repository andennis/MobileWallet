using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pass.Container.Core.Entities.Templates.PassTemplate
{
    public class LocationDetails
    {
        //IOS 7
        [JsonProperty(PropertyName = "maxDistance", NullValueHandling = NullValueHandling.Ignore)]
        public string MaxDistance { get; set; }

        [JsonProperty(PropertyName = "locations", NullValueHandling = NullValueHandling.Ignore)]
        public List<Location> Locations { get; set; }
    }
    
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
