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
        private string _url;

        public void Read(string url)
        {
            _url = url;
        }

        internal string Action { get { return _url; }}
    }
}
