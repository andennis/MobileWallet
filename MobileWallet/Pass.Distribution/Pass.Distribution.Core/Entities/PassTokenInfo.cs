using Newtonsoft.Json;

namespace Pass.Distribution.Core.Entities
{
    public class PassTokenInfo
    {
        [JsonProperty(PropertyName = "p")]
        public int? PassContentId { get; set; }

        [JsonProperty(PropertyName = "t")]
        public int? PassContentTemplateId { get; set; }
    }
}
