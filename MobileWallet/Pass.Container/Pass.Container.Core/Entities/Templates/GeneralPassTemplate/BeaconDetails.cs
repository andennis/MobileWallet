﻿using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Pass.Container.Core.Entities.Templates.GeneralPassTemplate
{
    public class BeaconDetails
    {
        [XmlArray(ElementName = "beacons", IsNullable = false)]
        [XmlArrayItem(ElementName = "beacon", IsNullable = false)]
        [JsonProperty(PropertyName = "beacons", NullValueHandling = NullValueHandling.Ignore)]
        public List<GeneralBeacon> Beacons { get; set; }
    }

    public class GeneralBeacon
    {
        [XmlElement(ElementName = "name")]
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [XmlElement(ElementName = "major")]
        [JsonProperty(PropertyName = "major", NullValueHandling = NullValueHandling.Ignore)]
        public int? Major { get; set; }

        [XmlElement(ElementName = "minor")]
        [JsonProperty(PropertyName = "minor", NullValueHandling = NullValueHandling.Ignore)]
        public int? Minor { get; set; }

        [XmlElement(ElementName = "proximityUUID")]
        [JsonProperty(PropertyName = "proximityUUID", Required = Required.Always)]
        public string ProximityUuid { get; set; }

        [XmlElement(ElementName = "relevantText")]
        [JsonProperty(PropertyName = "relevantText", NullValueHandling = NullValueHandling.Ignore)]
        public string RelevantText { get; set; }
    }
}
