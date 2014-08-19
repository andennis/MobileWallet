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
        private HtmlHelper _htmlHelper;
        private IEnumerable<T> _dataSource;
        private _GridViewModel _gridModel = new _GridViewModel();
        private string _partialViewName;
        private GridColumnFactory<T> _columnFactory = new GridColumnFactory<T>();

        public GridBuilder(HtmlHelper htmlHelper, string partialViewName)
            : this(htmlHelper, partialViewName, null)
        { 
        }
        public GridBuilder(HtmlHelper htmlHelper, string partialViewName, IEnumerable<T> dataSource)
        {
            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");

            _htmlHelper = htmlHelper;
            _dataSource = dataSource;
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

        public GridBuilder<T> BindTo(IEnumerable<T> dataSource)
        {
            _dataSource = dataSource;
            return this;
        }

        public GridBuilder<T> Columns(Action<GridColumnFactory<T>> configurator)
        {
            configurator(_columnFactory);
            return this;
        }

        public string ToHtmlString()
        {
            return _htmlHelper.Partial(_partialViewName, _gridModel).ToHtmlString();
        }
    }
}
