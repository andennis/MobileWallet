using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Common.Extensions;

namespace Common.Web.Grid
{
    public class GridColumnFactory<TModel> where TModel : class
    {
        private readonly HtmlHelper _htmlHelper;

        public GridColumnFactory(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
            this.Columns = new List<GridBoundColumnBuilder<TModel>>();
        }
        public IList<GridBoundColumnBuilder<TModel>> Columns { get; private set; }

        public GridBoundColumnBuilder<TModel> Bound()
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper);
            Columns.Add(builder);
            return builder;
        }

        public GridBoundColumnBuilder<TModel> Bound<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper, expression.GetMethodOrPropertyName(), typeof(TValue));
            Columns.Add(builder);
            return builder;
        }

        public GridBoundColumnBuilder<TModel> BoundLink<TValue, TId>(Expression<Func<TModel, TValue>> expression, string url, Expression<Func<TModel, TId>> expressionId)
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper, expression.GetMethodOrPropertyName());
            string idColName = expressionId.GetMethodOrPropertyName();

            int i = url.IndexOf('?');
            if (i != -1)
                url = url.Insert(i, string.Format("/#={0}#", idColName));
            else
                url = url + string.Format("/#={0}#", idColName);

            builder.ClientTemplate(string.Format("<a id=\"{0}.#={1}#\" href=\"{2}\">#={0}#</a>", builder.ColName, idColName, url));
            Columns.Add(builder);
            return builder;
        }

        public GridBoundColumnBuilder<TModel> BoundLink<TId>(string linkText, string url, Expression<Func<TModel, TId>> expressionId, string colName = null)
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper, colName);
            string idColName = expressionId.GetMethodOrPropertyName();

            int i = url.IndexOf('?');
            if (i != -1)
                url = url.Insert(i, string.Format("/#={0}#", idColName));
            else
                url = url + string.Format("/#={0}#", idColName);

            string template = string.IsNullOrEmpty(colName)
                ? string.Format("<a href=\"{0}\">{1}</a>", url, linkText)
                : string.Format("<a id=\"{0}.#={1}#\" href=\"{2}\">{3}</a>", colName, idColName, url, linkText);
            builder.ClientTemplate(template);
            Columns.Add(builder);
            return builder;
        }
        
    }
}
