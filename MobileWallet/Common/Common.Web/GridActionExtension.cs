using Common.Web.Grid;

namespace Common.Web
{
    public static class GridActionExtension
    {
        public static GridBoundColumnBuilder<TModel> GridAction<TModel>(this GridBoundColumnBuilder<TModel> columnBuilder, string linkText, string url, string name = null)
            where TModel : class
        {
            string template = columnBuilder.ColClientTemplate;
            string linkId = !string.IsNullOrEmpty(name) ? string.Format("{0}.#={1}#", name, columnBuilder.ColName) : string.Empty;
            if (!string.IsNullOrEmpty(template))
                template += "&nbsp;";
            template += string.Format("<a id=\"{0}\" href=\"{2}/#={1}#\">{3}</a>", linkId, columnBuilder.ColName, url, linkText);
            return columnBuilder.ClientTemplate(template);
        }
        public static GridBoundColumnBuilder<TModel> GridAjaxAction<TModel>(this GridBoundColumnBuilder<TModel> columnBuilder, string linkText, string url, string name = null)
            where TModel : class
        {
            string template = columnBuilder.ColClientTemplate;
            string linkId = !string.IsNullOrEmpty(name) ? string.Format("{0}.#={1}#", name, columnBuilder.ColName) : string.Empty;
            if (!string.IsNullOrEmpty(template))
                template += "&nbsp;";
            template += string.Format("<a id=\"{0}\" href=\"javascript:void(0)\" data-action=\"{2}/#={1}#\">{3}</a>", linkId, columnBuilder.ColName, url, linkText);
            return columnBuilder.ClientTemplate(template);
        }

    }
}
