using System.Collections.Generic;

namespace Common.Web.Controls.PanelBar
{
    public class PanelBarItem : NavigationItem<PanelBarItem>, INavigationItemContainer<PanelBarItem>
    {
        public PanelBarItem()
        {
            Items = new List<PanelBarItem>();
        }

        public bool Expanded { get; set; }
        public string ContentUrl { get; set; }
        public IList<PanelBarItem> Items { get; private set; }
    }
}
