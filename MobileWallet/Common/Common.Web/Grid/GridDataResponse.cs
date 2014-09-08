using System.Collections.Generic;
using System.Linq;

namespace Common.Web.Grid
{
    public class GridDataResponse
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IEnumerable<object> data { get; set; }
        /*
        public string sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public IEnumerable<object> aaData { get; set; }
        */
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
