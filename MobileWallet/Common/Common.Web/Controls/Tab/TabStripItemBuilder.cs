namespace Common.Web.Controls.Tab
{
    public class TabStripItemBuilder
    {
        private readonly TabStripItem _tabItem;

        public TabStripItemBuilder(TabStripItem item)
        {
            _tabItem = item;
        }

        public TabStripItemBuilder Text(string value)
        {
            _tabItem.Text = value;
            return this;
        }
        public TabStripItemBuilder Visible(bool value)
        {
            _tabItem.Visible = value;
            return this;
        }
        public TabStripItemBuilder Selected(bool value)
        {
            _tabItem.Selected = value;
            return this;
        }
        public TabStripItemBuilder Enabled(bool value)
        {
            _tabItem.Enabled = value;
            return this;
        }
        public TabStripItemBuilder Content(string value)
        {
            _tabItem.Content = value;
            return this;
        }
        public TabStripItemBuilder LoadContentFrom(string url)
        {
            _tabItem.LoadContentUrl = url;
            return this;
        }

    }
}
