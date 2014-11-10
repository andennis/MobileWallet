using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Pass.Manager.Web.Models.GeneralPassTemplate
{
    public class LocationDetails
    {
        //IOS 7
        [XmlElement(ElementName = "maxDistance")]
        [JsonProperty(PropertyName = "maxDistance", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxDistance { get; set; }

        [XmlArray(ElementName = "locations")]
        [XmlArrayItem(ElementName = "location")]
        [JsonProperty(PropertyName = "locations", NullValueHandling = NullValueHandling.Ignore)]
        public List<GeneralLocation> Locations { get; set; }
    }

    public class GeneralLocation
    {
        [XmlElement(ElementName = "isMarkedLocation")]
        [JsonProperty(PropertyName = "isMarkedLocation", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsMarkedLocation { get; set; }

        [XmlElement(ElementName = "address")]
        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [XmlElement(ElementName = "altitude")]
        [JsonProperty(PropertyName = "altitude", NullValueHandling = NullValueHandling.Ignore)]
        public double? Altitude { get; set; }

        [XmlElement(ElementName = "latitude")]
        [JsonProperty(PropertyName = "latitude", Required = Required.Always)]
        public double? Latitude { get; set; }

        [XmlElement(ElementName = "longitude")]
        [JsonProperty(PropertyName = "longitude", Required = Required.Always)]
        public double? Longitude { get; set; }

        [XmlElement(ElementName = "relevantText")]
        [JsonProperty(PropertyName = "relevantText", NullValueHandling = NullValueHandling.Ignore)]
        public string RelevantText { get; set; }
    }
}
