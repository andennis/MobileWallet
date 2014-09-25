using System;

namespace Common.Web.Grid
{
    public class GridBoundColumnBuilder<TModel> where TModel : class
    {
        public GridBoundColumnBuilder(string colName)
            :this(colName, typeof(string))
        {
        }
        public GridBoundColumnBuilder(string colName, Type colType)
        {
            ColName = colName;
            ColType = colType;
            ColTitle = colName;
            IsVisible = true;

            if (colType == typeof (DateTime))
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

        public GridBoundColumnBuilder<TModel> Format(string format)
        {
            ColFormat = format;
            return this;
        }

        public GridBoundColumnBuilder<TModel> ClientTemplate(string template)
        {
            ColClientTemplate = template;
            return this;
        }

        public GridBoundColumnBuilder<TModel> ClientTemplateId(string templateId)
        {
            ColClientTemplateId = templateId;
            return this;
        }
        
    }
}
