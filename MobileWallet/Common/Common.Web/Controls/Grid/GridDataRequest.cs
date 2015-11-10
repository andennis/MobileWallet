
namespace Common.Web.Controls.Grid
{
    public class GridDataRequest
    {
        //[JsonProperty(PropertyName = "draw")]
        public int draw { get; set; }

        //[JsonProperty(PropertyName = "start")]
        public int start { get; set; }

        //[JsonProperty(PropertyName = "length")]
        public int length { get; set; }

        public string sortColumn { get; set; }

        public string sortDirection { get; set; }
    }
}
