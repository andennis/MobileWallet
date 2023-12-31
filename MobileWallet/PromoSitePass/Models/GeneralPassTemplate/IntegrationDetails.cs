﻿using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Pass.Manager.Web.Models.GeneralPassTemplate
{
    public class IntegrationDetails
    {
        [XmlElement(ElementName = "appOptions")]
        [JsonProperty(PropertyName = "appOptions")]
        public AppOptions AppOptions { get; set; }

        [XmlElement(ElementName = "callbackNotifications")]
        [JsonProperty(PropertyName = "callbackNotifications")]
        public CallbackNotifications CallbackNotifications { get; set; }
    }

    public class CallbackNotifications
    {
        [XmlElement(ElementName = "passIssued")]
        [JsonProperty(PropertyName = "passIssued")]
        public string PassIssued { get; set; }

        [XmlElement(ElementName = "passRegistered")]
        [JsonProperty(PropertyName = "passRegistered")]
        public string PassRegistered { get; set; }

        [XmlElement(ElementName = "passUpdated")]
        [JsonProperty(PropertyName = "passUpdated")]
        public string PassUpdated { get; set; }

        [XmlElement(ElementName = "passUnregistered")]
        [JsonProperty(PropertyName = "passUnregistered")]
        public string PassUnregistered { get; set; }
    }

    public class AppOptions
    {
        [XmlArray(ElementName = "appIdentifier")]
        [XmlArrayItem(ElementName = "value")]
        [JsonProperty(PropertyName = "appIdentifier")]
        public List<int> AppIdentifier { get; set; }

        //IOS 7 
        [XmlElement(ElementName = "appLaunchUrl")]
        [JsonProperty(PropertyName = "appLaunchUrl")]
        public string AppLaunchUrl { get; set; }

        //IOS 7 ApplePass - userInfo
        [XmlElement(ElementName = "customJsonData")]
        [JsonProperty(PropertyName = "customJsonData")]
        public string CustomJsonData { get; set; }
    }
}
