using System;

namespace Common.Web.Controls.PanelBar
{
    public class PanelBarItemBuilder :  NavigationItemBuilder<PanelBarItem, PanelBarItemBuilder>
    {
        private readonly PanelBarItemFactory _iemaFactory;
        public PanelBarItemBuilder(PanelBarItem item/*, ViewContext viewContext*/)
            : base(item/*, viewContext*/)
        {
            _iemaFactory = new PanelBarItemFactory(item);
        }

        public PanelBarItemBuilder Items(Action<PanelBarItemFactory> addAction)
        {
            addAction(_iemaFactory);
            return this;
        }

        public PanelBarItemBuilder Expanded(bool value)
        {
            ((PanelBarItem) _item).Expanded = value;
            return this;
        }

    }
}
