using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
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

        /*
        public GridBoundColumnBuilder<TModel> Bound()
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper);
            Columns.Add(builder);
            return builder;
        }
        */

        public GridBoundColumnBuilder<TModel> Bound<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper, expression.GetPropertyName(), typeof(TValue));
            Columns.Add(builder);
            return builder;
        }

        public GridBoundColumnBuilder<TModel> BoundEnum<TValue>(Expression<Func<TModel, TValue>> expression) where TValue : struct
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper, expression.GetPropertyName(), typeof(TValue));

            var sbTemplate = new StringBuilder();
            IDictionary<string, int> enumDict = EnumHelper.ToDictionary<TValue>();
            foreach (KeyValuePair<string, int> item in enumDict)
            {
                sbTemplate.AppendFormat(" {0}if({1}=={2}) {{#{3}#}}", sbTemplate.Length > 0 ? "else " : string.Empty, builder.ColName, item.Value, item.Key);
            }

            builder.ClientTemplate(string.Format("# {0} #", sbTemplate));
            Columns.Add(builder);
            return builder;
        }

        public GridBoundColumnBuilder<TModel> BoundLink<TValue, TId>(Expression<Func<TModel, TValue>> expression, string url, Expression<Func<TModel, TId>> expressionId, 
            object htmlAttributes = null, string colTitle = null)
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper, expression.GetPropertyName());
            if (colTitle != null)
                builder.ColTitle = colTitle;

            string idColName = expressionId.GetPropertyName();

            int i = url.IndexOf('?');
            if (i != -1)
                url = url.Insert(i, string.Format("/#={0}#", idColName));
            else
                url = url + string.Format("/#={0}#", idColName);

            var tb = new TagBuilder("a");
            tb.Attributes.Add("id", string.Format("{0}.#={1}#", builder.ColName, idColName));
            tb.Attributes.Add("href", url);
            tb.SetInnerText(string.Format("#={0}#", builder.ColName));

            if (htmlAttributes != null)
                tb.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            builder.ClientTemplate(tb.ToString());
            Columns.Add(builder);
            return builder;
        }

        public GridBoundColumnBuilder<TModel> BoundLink<TId>(string linkText, string url, Expression<Func<TModel, TId>> expressionId, string colName = null)
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper, colName);
            string idColName = expressionId.GetPropertyName();

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
