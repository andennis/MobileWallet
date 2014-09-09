
namespace Common.Web.Grid
{
    public class GridBoundColumnBuilder<TModel> where TModel : class
    {
        public GridBoundColumnBuilder(string name)
        {
            ColName = name;
            ColTitle = name;
            IsVisible = true;
        }

        internal string ColWidth { get; private set; }
        internal string ColName { get; private set; }
        internal string ColTitle { get; set; }
        internal bool IsVisible { get; set; }
        internal string ColClientTemplate { get; set; }

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

        public GridBoundColumnBuilder<TModel> ClientTemplate(string template)
        {
            ColClientTemplate = template;
            return this;
        }

        
    }
}
