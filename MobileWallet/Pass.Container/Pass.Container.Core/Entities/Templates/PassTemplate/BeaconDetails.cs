using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Pass.Container.Core.Entities.Templates.PassTemplate
{
    public class BeaconDetails
    {
        [XmlArray(ElementName = "beacons")]
        [XmlArrayItem(ElementName = "beacon")]
        [JsonProperty(PropertyName = "beacons", NullValueHandling = NullValueHandling.Ignore)]
        public List<Beacon> Beacons { get; set; }
    }

    //Information about a location beacon
    public class Beacon
    {
        [XmlElement(ElementName = "name")]
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [XmlElement(ElementName = "major")]
        [JsonProperty(PropertyName = "major", NullValueHandling = NullValueHandling.Ignore)]
        public Int16? Major { get; set; }

        [XmlElement(ElementName = "minor")]
        [JsonProperty(PropertyName = "minor", NullValueHandling = NullValueHandling.Ignore)]
        public Int16? Minor { get; set; }

        [XmlElement(ElementName = "proximityUUID")]
        [JsonProperty(PropertyName = "proximityUUID", Required = Required.Always)]
        public string ProximityUuid { get; set; }

        [XmlElement(ElementName = "relevantText")]
        [JsonProperty(PropertyName = "relevantText", NullValueHandling = NullValueHandling.Ignore)]
        public string RelevantText { get; set; }
    }
}
