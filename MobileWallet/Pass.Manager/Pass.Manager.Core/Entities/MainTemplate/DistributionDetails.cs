using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pass.Manager.Core.Entities.MainTemplate
{
    public class DistributionDetails
    {
        [JsonProperty(PropertyName = "passLinkType", Required = Required.Always)]
        public PassLinkType PassLinkType { get; set; }

        [JsonProperty(PropertyName = "limitPassPerUser", NullValueHandling = NullValueHandling.Ignore)]
        public int? LimitPassPerUser { get; set; }

        [JsonProperty(PropertyName = "allPassesAsExpired")]
        public bool AllPassesAsExpired { get; set; }

        [JsonProperty(PropertyName = "autoExpirePassesAfter", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime AutoExpirePassesAfter { get; set; }

        [JsonProperty(PropertyName = "quantityRestriction", NullValueHandling = NullValueHandling.Ignore)]
        public int? QuantityRestriction { get; set; }

        [JsonProperty(PropertyName = "dateRestriction", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DateRestriction { get; set; }

        [JsonProperty(PropertyName = "passwordToIssue", NullValueHandling = NullValueHandling.Ignore)]
        public string PasswordToIssue { get; set; }

        [JsonProperty(PropertyName = "passwordToUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public string PasswordToUpdate { get; set; }
    }

    public enum PassLinkType
    {
        Public = 0,
        Private
    }
    
}
