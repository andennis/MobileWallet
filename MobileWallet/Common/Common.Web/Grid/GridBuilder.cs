using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Common.Web.Grid
{
    public class GridBuilder<T> : IHtmlString where T : class
    {
        private readonly HtmlHelper _htmlHelper;
        private readonly GridViewModel<T> _gridModel = new GridViewModel<T>();
        private readonly string _partialViewName;

        public GridBuilder(HtmlHelper htmlHelper, string partialViewName)
            : this(htmlHelper, partialViewName, null)
        { 
        }
        public GridBuilder(HtmlHelper htmlHelper, string partialViewName, IEnumerable<T> dataSource)
        {
            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");
            if (string.IsNullOrEmpty(partialViewName))
                throw new ArgumentException("partialViewName is required", "partialViewName");

            _htmlHelper = htmlHelper;
            _gridModel.DataSource = dataSource;
            _partialViewName = partialViewName;
        }

        public GridBuilder<T> Name(string name)
        {
            _gridModel.Name = name;
             return this;
        }

        public GridBuilder<T> Title(string title)
        {
            _gridModel.Title = title;
            return this;
        }
        public GridBuilder<T> Width(string width)
        {
            _gridModel.Width = width;
            return this;
        }
        public GridBuilder<T> Height(string height)
        {
            _gridModel.Height = height;
            return this;
        }
        public GridBuilder<T> BindTo(IEnumerable<T> dataSource)
        {
            _gridModel.DataSource = dataSource;
            return this;
        }

        public GridBuilder<T> Columns(Action<GridColumnFactory<T>> configurator)
        {
            configurator(_gridModel.ColumnFactory);
            return this;
        }

        public string ToHtmlString()
        {
            return _htmlHelper.Partial(_partialViewName, _gridModel).ToHtmlString();
        }
    }
}
