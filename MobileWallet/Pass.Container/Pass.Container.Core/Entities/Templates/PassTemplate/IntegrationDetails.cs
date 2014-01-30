using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pass.Container.Core.Entities.Templates.PassTemplate
{
    public class IntegrationDetails
    {
        [JsonProperty(PropertyName = "appOptions")]
        public AppOptions AppOptions { get; set; }

        [JsonProperty(PropertyName = "callbackNotifications")]
        public CallbackNotifications CallbackNotifications { get; set; }
    }

    public class CallbackNotifications
    {
        [JsonProperty(PropertyName = "passIssued")]
        public string PassIssued { get; set; }

        [JsonProperty(PropertyName = "passRegistered")]
        public string PassRegistered { get; set; }

        [JsonProperty(PropertyName = "passUpdated")]
        public string PassUpdated { get; set; }

        [JsonProperty(PropertyName = "passIssued")]
        public string PassUnregistered { get; set; }
    }

    public class AppOptions
    {
        [JsonProperty(PropertyName = "appIdentifier")]
        public List<int> AppIdentifier { get; set; }

        //IOS 7 
        [JsonProperty(PropertyName = "appLaunchUrl")]
        public Uri AppLaunchUrl { get; set; }

        //IOS 7 ApplePass - userInfo
        [JsonProperty(PropertyName = "customJsonData")]
        public string CustomJsonData { get; set; }
    }
}
