using Newtonsoft.Json;

namespace Common.Web.Controls.DatePicker
{
    public class MonthTemplate
    {
        [JsonProperty(PropertyName = "idPrefix")]
        public string IdPrefix { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "empty")]
        public string Empty { get; set; }

        [JsonProperty(PropertyName = "contentId")]
        public string ContentId { get; set; }

        [JsonProperty(PropertyName = "emptyId")]
        public string EmptyId { get; set; }
    }
}
