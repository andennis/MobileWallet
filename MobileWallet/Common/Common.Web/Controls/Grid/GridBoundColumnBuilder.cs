using System;
using System.Web.Mvc;

namespace Common.Web.Controls.Grid
{
    public class GridBoundColumnBuilder<TModel> where TModel : class
    {
        private readonly HtmlHelper _htmlHelper;

        public GridBoundColumnBuilder(HtmlHelper htmlHelper, string colName = null)
            : this(htmlHelper, colName, typeof(string))
        {
        }
        public GridBoundColumnBuilder(HtmlHelper htmlHelper, string colName, Type colType)
        {
            _htmlHelper = htmlHelper;

            ColName = colName;
            ColType = colType;
            ColTitle = colName;
            IsVisible = true;

            if (colType == typeof (DateTime) || colType == typeof(DateTime?))
                ColFormat = "d";
        }

        internal string ColWidth { get; private set; }
        internal string ColName { get; private set; }
        internal Type ColType { get; set; }
        internal string ColTitle { get; set; }
        internal bool IsVisible { get; set; }
        internal string ColFormat { get; set; }
        internal string ColClientTemplate { get; set; }
        internal string ColClientTemplateId { get; set; }

        public GridBoundColumnBuilder<TModel> Width(string width)
        {
            ColWidth = width;
            return this;
        }
        public GridBoundColumnBuilder<TModel> Title(string title)
        {
            ColTitle = title;
            return this;
        }

        public GridBoundColumnBuilder<TModel> Visible(bool isVisible)
        {
            IsVisible = isVisible;
            return this;
        }

        /// <summary>
        /// Date time format description: http://docs.telerik.com/kendo-ui/framework/globalization/dateformatting
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public GridBoundColumnBuilder<TModel> Format(string format)
        {
            ColFormat = format;
            return this;
        }

        /// <summary>
        /// The guide to build client template: http://docs.telerik.com/kendo-ui/framework/templates/overview
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public GridBoundColumnBuilder<TModel> ClientTemplate(string template)
        {
            ColClientTemplate = template;
            return this;
        }

        /// <summary>
        /// The guide to build client template: http://docs.telerik.com/kendo-ui/framework/templates/overview
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public GridBoundColumnBuilder<TModel> ClientTemplateId(string templateId)
        {
            ColClientTemplateId = templateId;
            return this;
        }

    }
}
