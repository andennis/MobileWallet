using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Web.Grid
{
    public class GridViewModel<TModel> where TModel : class
    {
        public GridViewModel()
        {
            ColumnFactory = new GridColumnFactory<TModel>();
            DataSourceFactory = new GridDataSourceFactory<TModel>();
        }

        public string Name { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public bool Pageable { get; set; }
        public bool Sortable { get; set; }
        public GridColumnFactory<TModel> ColumnFactory { get; private set; }
        public GridDataSourceFactory<TModel> DataSourceFactory { get; private set; }
        public IEnumerable<TModel> DataSource { get; set; }
    }
}