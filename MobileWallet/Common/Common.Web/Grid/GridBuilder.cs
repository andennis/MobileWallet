﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Common.Extensions;

namespace Common.Web.Grid
{
    public class GridBuilder<T> : IHtmlString where T : class
    {
        private readonly HtmlHelper _htmlHelper;
        private readonly GridViewModel<T> _gridModel = new GridViewModel<T>();

        public GridBuilder(HtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");

            _htmlHelper = htmlHelper;
        }

        public GridBuilder<T> Name(string name)
        {
            _gridModel.Name = name;
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

        /*
        public GridBuilder<T> BindTo(IEnumerable<T> dataSource)
        {
            _gridModel.DataSource = dataSource;
            return this;
        }
        */

        public GridBuilder<T> Columns(Action<GridColumnFactory<T>> configurator)
        {
            configurator(_gridModel.ColumnFactory);
            return this;
        }

        public GridBuilder<T> DataSource(Action<GridDataSourceFactory<T>> configurator)
        {
            configurator(_gridModel.DataSourceFactory);
            return this;
        }

        public GridBuilder<T> Pageable()
        {
            _gridModel.Pageable = true;
            return this;
        }
        public GridBuilder<T> Sortable()
        {
            _gridModel.Sortable = true;
            return this;
        }

        public string ToHtmlString()
        {
            return RenderGrid();
        }

        private string RenderGrid()
        {
            //<table>
            var sb = new StringBuilder();
            var tableTag = new TagBuilder("table");
            tableTag.GenerateId(_gridModel.Name);

            string style;

            if (!string.IsNullOrEmpty(_gridModel.Width))
                tableTag.Attributes.Add("style", "width:" + _gridModel.Width + ";");
            if (!string.IsNullOrEmpty(_gridModel.Height))
                tableTag.Attributes["style"] += "height:" + _gridModel.Height;
                
            sb.AppendLine(tableTag.ToString());

            //<script>
            sb.AppendLine(GetGridInitializationScript());

            return sb.ToString();
        }

        private string GetGridInitializationScript()
        {
            var columns = _gridModel.ColumnFactory.Columns.Select(x => new {mData = x.ColName, sTitle = x.ColTitle});
            
            var tableSettings = new
                                {
                                    bServerSide = true,
                                    sAjaxSource = _gridModel.DataSourceFactory.Action,
                                    bProcessing = true,
                                    bFilter = false,
                                    bSort = _gridModel.Sortable,
                                    bPaginate = _gridModel.Pageable,
                                    bInfo = false,
                                    oLanguage = new { sZeroRecords = "No data"},
                                    aoColumns = columns
                                };

            var scriptTag = new TagBuilder("script");
            //scriptTag.Attributes.Add("type", "text/javascript");
            scriptTag.InnerHtml = @"$(document).ready(function () {
                $('#" + _gridModel.Name + @"').dataTable(" + tableSettings.ObjectToJson() + @");
            })";

            return scriptTag.ToString();
        }
    }
}