using Common.Extensions.JsonNetConverters;
using Newtonsoft.Json;

namespace Common.Web.Grid
{
    internal class GridColumn
    {
        [JsonProperty(PropertyName = "data")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "visible")]
        public bool Visible { get; set; }

        [JsonProperty(PropertyName = "width")]
        public string Width { get; set; }

        /*
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        */

        [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
        [JsonProperty(PropertyName = "render")]
        public string Render { get; set; }
    }
}
