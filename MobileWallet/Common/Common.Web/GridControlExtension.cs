using System.Web.Mvc;
using Common.Web.Controls.Grid;

namespace Common.Web
{
    public static class GridControlExtension
    {
        public static GridBuilder<TModel> Grid<TModel>(this HtmlHelper html) where TModel : class
        {
            return html.Widget().Grid<TModel>();
        }
    }
}
