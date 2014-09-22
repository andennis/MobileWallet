using System.Collections.Generic;
using System.Linq;

namespace Common.Web.Grid
{
    public class GridDataResponse
    {
        //[JsonProperty(PropertyName = "draw")]
        public int draw { get; set; }

        //[JsonProperty(PropertyName = "recordsTotal")]
        public int recordsTotal { get; set; }

        //[JsonProperty(PropertyName = "recordsFiltered")]
        public int recordsFiltered { get; set; }

        //[JsonProperty(PropertyName = "data")]
        public IEnumerable<object> data { get; set; }

        public static GridDataResponse Create(GridDataRequest request, IEnumerable<object> data, int totalRecordsCount = 0)
        {
            int recordsCount = 0;
            if (data != null)
                recordsCount = totalRecordsCount != 0 ? totalRecordsCount : data.Count();

            var resp = new GridDataResponse()
                       {
                           draw = request.draw,
                           data = data,
                           recordsTotal = recordsCount,
                           recordsFiltered = recordsCount
                       };
            return resp;
        }
    }
}
