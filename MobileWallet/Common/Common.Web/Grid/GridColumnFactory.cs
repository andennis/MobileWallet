using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public GridBoundColumnBuilder<TModel> BoundLink<TValue, TId>(Expression<Func<TModel, TValue>> expression, string url, Expression<Func<TModel, TId>> expressionId)
        {
            var builder = new GridBoundColumnBuilder<TModel>(expression.GetMethodOrPropertyName());
            string idColName = expressionId.GetMethodOrPropertyName();
            builder.ClientTemplate(string.Format("<a id=\"{0}.#={1}#\" href=\"{2}/#={1}#\">#={0}#</a>", builder.ColName, idColName, url));
            Columns.Add(builder);
            return builder;
        }

        public GridBoundColumnBuilder<TModel> BoundLink<TId>(string text, string url, Expression<Func<TModel, TId>> expressionId, string colName = "")
        {
            var builder = new GridBoundColumnBuilder<TModel>(colName);
            string idColName = expressionId.GetMethodOrPropertyName();
            builder.ClientTemplate(string.Format("<a href=\"{1}/#={0}#\">{2}</a>", idColName, url, text));
            Columns.Add(builder);
            return builder;
        }

    }
}
