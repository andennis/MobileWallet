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
        }

        public string Name { get; set; }
        public string Title { get; set; }

        public string Width { get; set; }
        public string Height { get; set; }
        public GridColumnFactory<TModel> ColumnFactory { get; set; }
        public IEnumerable<TModel> DataSource { get; set; }
    }
}