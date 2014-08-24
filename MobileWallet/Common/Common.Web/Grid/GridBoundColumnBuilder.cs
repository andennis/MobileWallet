using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Web.Grid
{
    public class GridBoundColumnBuilder<TModel> where TModel : class
    {
        public GridBoundColumnBuilder(string name)
        {
            ColName = name;
            ColTitle = name;
        }

        internal string ColName { get; private set; }
        internal string ColTitle { get; set; }

        /*
        public GridBoundColumnBuilder<TModel> Name(string name)
        {
            ColName = name;
            return this;
        }
        */
        public GridBoundColumnBuilder<TModel> Title(string title)
        {
            ColTitle = title;
            return this;
        }
    }
}
