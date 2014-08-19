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
        public GridBoundColumnBuilder<TModel> Title(string title)
        {
            return this;
        }
    }
}
