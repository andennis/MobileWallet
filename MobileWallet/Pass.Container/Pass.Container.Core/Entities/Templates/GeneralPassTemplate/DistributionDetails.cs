﻿using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Pass.Container.Core.Entities.Templates.GeneralPassTemplate
{
    public class DistributionDetails
    {
        /*
        [XmlElement(ElementName = "passLinkType")]
        [JsonProperty(PropertyName = "passLinkType", Required = Required.Always)]
        public PassLinkType PassLinkType { get; set; }

        [XmlElement(ElementName = "limitPassPerUser")]
        [JsonProperty(PropertyName = "limitPassPerUser", NullValueHandling = NullValueHandling.Ignore)]
        public int? LimitPassPerUser { get; set; }
        */

        /*
        [XmlElement(ElementName = "allPassesAsExpired")]
        [JsonProperty(PropertyName = "allPassesAsExpired", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllPassesAsExpired { get; set; }//??

        [XmlElement(ElementName = "expirationDate")]
        [JsonProperty(PropertyName = "expirationDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ExpirationDate { get; set; }
        */
    }

    /*
    public enum PassLinkType
    {
        [XmlEnum(Name = "public")]
        Public = 0,
        [XmlEnum(Name = "private")]
        Private
    }
    */

}
