using System.Collections.Generic;

namespace Common.Web.Controls.Grid
{
    public class GridDataRequest
    {
        //[JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        //[JsonProperty(PropertyName = "start")]
        public int Start { get; set; }

        //[JsonProperty(PropertyName = "length")]
        public int Length { get; set; }

        public DTSearch Search { get; set; }

        public List<DTOrder> Order { get; set; }

        public List<DTColumn> Columns { get; set; }
    }

    public sealed class DTSearch
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }
    public sealed class DTOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public sealed class DTColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Orderable { get; set; }
        public DTSearch Search { get; set; }
    }
}
