using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Common.Web.Grid
{
    public static class GridControlExtension
    {
        public static GridBuilder<TModel> Grid<TModel>(this HtmlHelper html) where TModel : class
        {
            return new GridBuilder<TModel>(html);
        }
    }
}
