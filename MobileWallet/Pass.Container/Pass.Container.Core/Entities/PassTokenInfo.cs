using Newtonsoft.Json;

namespace Pass.Container.Core.Entities
{
    public class PassTokenInfo
    {
        [JsonProperty(PropertyName = "psId")]
        public int? PassId { get; set; }

        [JsonProperty(PropertyName = "pstId")]
        public int? PassTemplateId { get; set; }
    }
}
