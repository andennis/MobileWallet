using System.Collections.Generic;
using Common.Web.Controls.PanelBar;

namespace Common.Web.Controls
{
    public interface INavigationItemContainer<T> where T : NavigationItem<T>
    {
        IList<T> Items { get; }
    }
}
