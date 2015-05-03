using System.Web.Mvc;

namespace Common.Web.Controls.Grid
{
    public static class GridControlExtension
    {
        public static GridBuilder<TModel> Grid<TModel>(this HtmlHelper html) where TModel : class
        {
            return new GridBuilder<TModel>(html);
        }
    }
}
