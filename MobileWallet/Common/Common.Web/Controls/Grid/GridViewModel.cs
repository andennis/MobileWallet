using System.Collections.Generic;
using System.Web.Mvc;

namespace Common.Web.Controls.Grid
{
    public class GridViewModel<TModel> where TModel : class
    {
        private readonly HtmlHelper _htmlHelper;

        public GridViewModel(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;

            ColumnFactory = new GridColumnFactory<TModel>(htmlHelper);
            DataSourceFactory = new GridDataSourceFactory<TModel>();
            EventBuilder = new GridEventBuilder();

            IsAutoBind = true;
        }

        public string Name { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string CssClass { get; set; }
        public bool Pageable { get; set; }
        public bool Sortable { get; set; }
        public bool IsAutoBind { get; set; }
        public GridColumnFactory<TModel> ColumnFactory { get; private set; }
        public GridDataSourceFactory<TModel> DataSourceFactory { get; private set; }
        public IEnumerable<TModel> DataSource { get; set; }
        public GridEventBuilder EventBuilder { get; private set; }
    }
}