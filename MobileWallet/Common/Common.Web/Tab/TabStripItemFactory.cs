
using System.Collections.Generic;

namespace Common.Web.Tab
{
    public class TabStripItemFactory
    {
        internal IList<TabStripItem> Items { get; private set; }

        public TabStripItemFactory()
        {
            Items = new List<TabStripItem>();
        }

        public virtual TabStripItemBuilder Add()
        {
            var item = new TabStripItem();
            Items.Add(item);
            return new TabStripItemBuilder(item);
        }
    }
}
