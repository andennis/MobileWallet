using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Common.Extensions;

namespace Common.Web.Controls.Grid
{
    public class GridBuilder<T> : IHtmlString where T : class
    {
        private readonly HtmlHelper _htmlHelper;
        private readonly GridViewModel<T> _gridModel;

        public GridBuilder(HtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");

            _htmlHelper = htmlHelper;
            _gridModel = new GridViewModel<T>(htmlHelper);
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

        public GridBuilder<T> CssClass(string cssClass)
        {
            _gridModel.CssClass = cssClass;
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
        public GridBuilder<T> AutoBind(bool autoBind)
        {
            _gridModel.IsAutoBind = autoBind;
            return this;
        }

        public GridBuilder<T> Events(Action<GridEventBuilder> configurator)
        {
            configurator(_gridModel.EventBuilder);
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

            tableTag.AddCssClass("table table-bordered table-striped");
            if (!string.IsNullOrEmpty(_gridModel.CssClass))
                tableTag.AddCssClass(_gridModel.CssClass);

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
            var tblColumns = _gridModel.ColumnFactory.Columns
                .Select(x => new GridColumn
                             {
                                 Name = x.ColName, 
                                 Title = x.ColTitle, 
                                 Visible = x.IsVisible,
                                 Orderable = x.IsOrderable,
                                 Width = x.ColWidth,
                                 //Type = GetColumnTypeName(x.ColType),
                                 Render = GetRenderJsFunc(x)
                             });
            
            var tableSettings = new
                                {
                                    serverSide = true,
                                    deferLoading = _gridModel.IsAutoBind ? (int?) null : 0,
                                    ajax = new GridAjaxRequest()
                                           {
                                               Url = _gridModel.DataSourceFactory.Action, 
                                               Data = _gridModel.DataSourceFactory.DataHandler
                                           },
                                    processing = true,
                                    searching = false,
                                    ordering = _gridModel.Sortable,
                                    paging = _gridModel.Pageable,
                                    info = false,
                                    language = new { emptyTable = "No data available" },
                                    columns = tblColumns
                                };

            var events = new StringBuilder();
            if (!string.IsNullOrEmpty(_gridModel.EventBuilder.DataReadingHandler))
                events.AppendFormat(".on('preXhr.dt', function(e, settings, data){{{0}(e, data)}})", _gridModel.EventBuilder.DataReadingHandler);
            if (!string.IsNullOrEmpty(_gridModel.EventBuilder.DataBindingHandler))
                events.AppendFormat(".on('xhr.dt', function(e, settings, json){{{0}(e, json)}})", _gridModel.EventBuilder.DataBindingHandler);
            if (!string.IsNullOrEmpty(_gridModel.EventBuilder.DataBoundHandler))
                events.AppendFormat(".on('draw.dt', function(e, settings){{{0}(e)}})", _gridModel.EventBuilder.DataBoundHandler);

            var scriptTag = new TagBuilder("script");
            //scriptTag.Attributes.Add("type", "text/javascript");
            scriptTag.InnerHtml = @"$(document).ready(function () {
                $('#" + _gridModel.Name + @"')"+events+@".dataTable(" + tableSettings.ObjectToJson() + @");
            })";

            return scriptTag.ToString();
        }

        /*
        private string GetColumnTypeName(Type type)
        {
            return type == typeof (DateTime) ? "date" : null;
        }
        */

        private string GetRenderJsFunc(GridBoundColumnBuilder<T> colBuilder)
        {
            return (colBuilder.ColType == typeof(DateTime) || colBuilder.ColType == typeof(DateTime?))
                ? GetRenderJsFuncDateTimeFormat(colBuilder.ColFormat)
                : GetRenderJsFuncByClientTemplate(colBuilder);
        }

        private string GetRenderJsFuncByClientTemplate(GridBoundColumnBuilder<T> colBuilder)
        {
            if (!string.IsNullOrEmpty(colBuilder.ColClientTemplate) && !string.IsNullOrEmpty(colBuilder.ColClientTemplateId))
                throw new Exception(string.Format("Grid column ('{0}') template defenition contains both client tamplate and client tamplate ID. There should be only one defined", 
                    colBuilder.ColName));

            if (!string.IsNullOrEmpty(colBuilder.ColClientTemplate))
                return GetRenderJsFuncByClientTemplate(colBuilder.ColClientTemplate);

            if (!string.IsNullOrEmpty(colBuilder.ColClientTemplateId))
                return GetRenderJsFuncByClientTemplateId(colBuilder.ColClientTemplateId);

            return null;
        }

        private string GetRenderJsFuncByClientTemplateId(string clientTemplateId)
        {
            if (string.IsNullOrEmpty(clientTemplateId))
                return null;

            return @"function(data, type, row) {
                            var templateContent = $(""#"+clientTemplateId+ @""").html();
                            var template = kendo.template(templateContent);
                            return template(row);
                        }";
        }

        private string GetRenderJsFuncByClientTemplate(string clientTemplate)
        {
            if (string.IsNullOrEmpty(clientTemplate))
                return null;

            return @"function(data, type, row) {
                            var template = kendo.template('"+clientTemplate+ @"');
                            return template(row);
                        }";
        }

        private string GetRenderJsFuncDateTimeFormat(string dateTimeFormat)
        {
            if (string.IsNullOrEmpty(dateTimeFormat))
                return null;

            return @"function(data, type, row) {
                        if (data) {
                            var date = eval(data.replace(/\/Date\((-?\d+)\)\//gi, ""new Date($1)""));
                            return kendo.toString(date, """+dateTimeFormat+ @""");
                        }
                        return null;
                     }";
            /*
            return @"function(data, type, row) {
                        var date = moment(data).toDate();
                        return moment(data).format('" + dateTimeFormat + @"');
                     }";
            */
        }

        /*
        private string GetRenderJsFuncByClientTemplate(string clientTemplate)
        {
            if (string.IsNullOrEmpty(clientTemplate))
                return null;

            clientTemplate = Regex.Replace(clientTemplate, @"#=\s*(\S+?)\s*#", "'+row.$1+'");

            return @"function(data, type, row) {
                            return '"+clientTemplate+@"';
                        }";
        }
        */
    }
}
