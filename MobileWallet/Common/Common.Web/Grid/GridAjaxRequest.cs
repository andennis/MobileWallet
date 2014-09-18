using Common.Extensions.JsonNetConverters;
using Newtonsoft.Json;

namespace Common.Web.Grid
{
    internal class GridAjaxRequest
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
