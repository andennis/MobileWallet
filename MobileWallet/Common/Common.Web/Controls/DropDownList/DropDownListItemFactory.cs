using System.Collections.Generic;

namespace Common.Web.Controls.DropDownList
{
    public class DropDownListItemFactory
    {
        private readonly IList<DropDownListItem> _container;

        public DropDownListItemFactory(IList<DropDownListItem> container)
        {
            _container = container;
        }

        public DropDownListItemBuilder Add()
        {
            var item = new DropDownListItem();
            _container.Add(item);
            return new DropDownListItemBuilder(item);
        }

    }
}
