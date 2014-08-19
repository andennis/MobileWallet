using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Web.Grid
{
    public class _GridViewModel
    {
        public _GridViewModel()
        {
            Columns = new List<_GridColumnViewModel>();
            Rows = new List<_GridRowViewModel>();
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public IList<_GridColumnViewModel> Columns { get; set; }
        public IEnumerable<_GridRowViewModel> Rows { get; set; }
    }
}