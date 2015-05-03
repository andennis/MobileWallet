using Newtonsoft.Json;

namespace Common.Web.Controls.Popup
{
    public class WindowPositionSettings
    {
        [JsonProperty(PropertyName = "top")]
        public int Top { get; set; }

        [JsonProperty(PropertyName = "left")]
        public int Left { get; set; }
    }
}
