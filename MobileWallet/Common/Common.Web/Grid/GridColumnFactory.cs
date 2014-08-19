using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Web.Grid
{
    public class GridColumnFactory<TModel> where TModel : class
    {
        public GridBoundColumnBuilder<TModel> Bound<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var builder = new GridBoundColumnBuilder<TModel>();
            return builder;
        }

    }
}
