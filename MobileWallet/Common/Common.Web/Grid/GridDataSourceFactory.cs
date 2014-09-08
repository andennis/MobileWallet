using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Common.Web.Grid
{
    public class GridDataSourceFactory<TModel> where TModel : class
    {
        public GridDataSourceFactory<TModel> Read(string url)
        {
            Action = url;
            return this;
        }

        public void Data(string dataHandler)
        {
            DataHandler = dataHandler;
        }

        internal string Action { get; set; }
        internal string DataHandler { get; set; }
    }
}
