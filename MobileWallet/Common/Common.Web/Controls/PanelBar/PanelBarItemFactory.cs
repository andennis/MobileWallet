
namespace Common.Web.Controls.PanelBar
{
    public class PanelBarItemFactory
    {
        private readonly INavigationItemContainer<PanelBarItem> _container;
        //private readonly ViewContext _viewContext;

        public PanelBarItemFactory(INavigationItemContainer<PanelBarItem> container/*, ViewContext viewContext*/)
        {
            _container = container;
            //_viewContext = viewContext;
        }

        public virtual PanelBarItemBuilder Add()
        {
            var item = new PanelBarItem();
            _container.Items.Add(item);
            return new PanelBarItemBuilder(item/*, _viewContext*/);
        }
    }
}
