using System.Web.Mvc;
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

            int i = url.IndexOf('?');
            if (i != -1)
                url = url.Insert(i, string.Format("/#={0}#", columnBuilder.ColName));
            else
                url = url + string.Format("/#={0}#", columnBuilder.ColName);

            template += string.Format("<a id=\"{0}\" href=\"{1}\">{2}</a>", linkId, url, linkText);
            return columnBuilder.ClientTemplate(template);
        }
        public static GridBoundColumnBuilder<TModel> GridAjaxAction<TModel>(this GridBoundColumnBuilder<TModel> columnBuilder, string linkText, string url, string name = null, string confirmMessage = null)
            where TModel : class
        {
            string template = columnBuilder.ColClientTemplate;
            string linkId = !string.IsNullOrEmpty(name) ? string.Format("{0}.#={1}#", name, columnBuilder.ColName) : string.Empty;

            if (!string.IsNullOrEmpty(template))
                template += "&nbsp;";

            int i = url.IndexOf('?');
            if (i != -1)
                url = url.Insert(i, string.Format("/#={0}#", columnBuilder.ColName));
            else
                url = url + string.Format("/#={0}#", columnBuilder.ColName);

            var tb = new TagBuilder("a");
            if (linkId != string.Empty)
                tb.Attributes.Add("id", linkId);
            tb.Attributes.Add("href", "javascript:void(0)");
            tb.Attributes.Add("data-grid-action", url);
            if (string.IsNullOrEmpty(confirmMessage))
                confirmMessage = string.Format("Are you sure you want to {0}?", linkText.ToLower());
            tb.Attributes.Add("confirmMessage", confirmMessage);
            tb.SetInnerText(linkText);

            template += tb.ToString();
            return columnBuilder.ClientTemplate(template);
        }

    }
}
