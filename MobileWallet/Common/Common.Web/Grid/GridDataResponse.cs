using System.Collections.Generic;
using System.Linq;

namespace Common.Web.Grid
{
    public class GridDataResponse
    {
        public string sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public IEnumerable<object> aaData { get; set; }

        public static GridDataResponse Create(GridDataRequest request, IEnumerable<object> data, int totalRecordsCount = 0)
        {
            int recordsCount = 0;
            if (data != null)
                recordsCount = totalRecordsCount != 0 ? totalRecordsCount : data.Count();

            var resp = new GridDataResponse()
                       {
                           sEcho = request.sEcho,
                           aaData = data,
                           iTotalRecords = recordsCount,
                           iTotalDisplayRecords = recordsCount
                       };
            return resp;
        }
    }
}
