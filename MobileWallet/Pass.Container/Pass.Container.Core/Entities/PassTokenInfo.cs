using Newtonsoft.Json;

namespace Pass.Container.Core.Entities
{
    public class PassTokenInfo
    {
        [JsonProperty(PropertyName = "psId")]
        public string PassId { get; set; }

        [JsonProperty(PropertyName = "pstId")]
        public string PassTemplateId { get; set; }
    }
}
