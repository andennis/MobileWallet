using Newtonsoft.Json;

namespace Pass.Distribution.Web
{
    public class PassTokenInfo
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
    }
}
