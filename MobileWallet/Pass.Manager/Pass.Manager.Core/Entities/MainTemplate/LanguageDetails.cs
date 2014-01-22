using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pass.Manager.Core.Entities.MainTemplate
{
    public class LanguageDetails
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
