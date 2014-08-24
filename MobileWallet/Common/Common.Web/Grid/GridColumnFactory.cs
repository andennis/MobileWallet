using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;

namespace Common.Web.Grid
{
    public class GridColumnFactory<TModel> where TModel : class
    {
        public GridColumnFactory()
        {
            this.Columns = new List<GridBoundColumnBuilder<TModel>>();
        }
        public IList<GridBoundColumnBuilder<TModel>> Columns { get; private set; }
    
        public GridBoundColumnBuilder<TModel> Bound<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var builder = new GridBoundColumnBuilder<TModel>(expression.GetMethodOrPropertyName());
            Columns.Add(builder);
            return builder;
        }

    }
}
