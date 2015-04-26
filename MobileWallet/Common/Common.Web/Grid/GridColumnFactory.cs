using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
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

        public GridBoundColumnBuilder<TModel> Bound<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper, expression.GetPropertyName(), typeof(TValue));
            Columns.Add(builder);
            return builder;
        }

        public GridBoundColumnBuilder<TModel> BoundEnum<TValue>(Expression<Func<TModel, TValue>> expression) where TValue : struct
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper, expression.GetPropertyName(), typeof(TValue));
            var sbTemplate = GetColEnumClientTemplete<TValue>(builder.ColName);
            builder.ClientTemplate(string.Format("# {0} #", sbTemplate));
            Columns.Add(builder);
            return builder;
        }
        public GridBoundColumnBuilder<TModel> BoundEnum<TValue>(Expression<Func<TModel, TValue?>> expression) where TValue : struct
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper, expression.GetPropertyName(), typeof(TValue));
            var sbTemplate = GetColEnumClientTemplete<TValue>(builder.ColName);
            builder.ClientTemplate(string.Format("# {0} #", sbTemplate));
            Columns.Add(builder);
            return builder;
        }

        public GridBoundColumnBuilder<TModel> BoundEnumLink<TValue, TId>(Expression<Func<TModel, TValue>> expression, string url, Expression<Func<TModel, TId>> expressionId,
            object htmlAttributes = null, string colTitle = null)
            where TValue : struct
        {
            string colName = expression.GetPropertyName();
            var template = GetColEnumClientTemplete<TValue>(colName);
            return BoundLink(colName, url, expressionId.GetPropertyName(), template, htmlAttributes, colTitle);
        }

        public GridBoundColumnBuilder<TModel> BoundLink<TValue, TId>(Expression<Func<TModel, TValue>> expression, string url, Expression<Func<TModel, TId>> expressionId, 
            object htmlAttributes = null, string colTitle = null)
        {
            return BoundLink(expression.GetPropertyName(), url, expressionId.GetPropertyName(), null, htmlAttributes, colTitle);
        }

        private GridBoundColumnBuilder<TModel> BoundLink(string colName, string url, string idColName, string colTemplate, object htmlAttributes, string colTitle)
        {
            var builder = new GridBoundColumnBuilder<TModel>(_htmlHelper, colName);
            if (colTitle != null)
                builder.ColTitle = colTitle;

            int i = url.IndexOf('?');
            if (i != -1)
                url = url.Insert(i, string.Format("/#={0}#", idColName));
            else
                url = url + string.Format("/#={0}#", idColName);

            var tb = new TagBuilder("a");
            tb.Attributes.Add("id", string.Format("{0}.#={1}#", builder.ColName, idColName));
            tb.Attributes.Add("href", url);

            string innerText = string.IsNullOrEmpty(colTemplate) ? string.Format("#={0}#", builder.ColName) : string.Format("# {0} #", colTemplate);
            tb.SetInnerText(innerText);

            if (htmlAttributes != null)
                tb.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            builder.ClientTemplate(tb.ToString());
            Columns.Add(builder);
            return builder;
        }

        private string GetColEnumClientTemplete<TEnum>(string colName) where TEnum : struct
        {
            var sb = new StringBuilder();
            IDictionary<string, int> enumDict = EnumHelper.ToDictionary<TEnum>();
            foreach (KeyValuePair<string, int> item in enumDict)
            {
                sb.AppendFormat(" {0}if({1}=={2}) {{#{3}#}}", (sb.Length > 0 ? "else " : string.Empty), colName, item.Value, item.Key);
            }
            return sb.ToString();
        }
        
    }
}
