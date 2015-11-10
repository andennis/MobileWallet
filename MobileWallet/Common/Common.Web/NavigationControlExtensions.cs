using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Common.Configuration.Menu;
using Common.Web.Controls.PanelBar;

namespace Common.Web
{
    public static class NavigationControlExtensions
    {
        private static DateTime _leftMenuLastUpdateDate = DateTime.MinValue;
        private static LeftMenuConfiguration _leftMenuConfiguration;

        private class LeftMenuConfiguration
        {
            public MenuConfiguration MenuConfiguration { get; set; }
            public IDictionary<string, IndexedMenuItem> IndexedMenuItems { get; set; }
        }
        private class IndexedMenuItem
        {
            public Menu ParentMenu { get; set; }
            public IndexedMenuItem ParentMenuItem { get; set; }
            public MenuItem Item { get; set; }
        }

        public static PanelBarBuilder PanelBar(this HtmlHelper htmlHelper)
        {
            return htmlHelper.Widget().PanelBar();
        }

        public static bool IsLeftMenu(this HtmlHelper htmlHelper, string menuConfigFile)
        {
            LeftMenuConfiguration menuConfig = GetLeftMenuConfiguration(menuConfigFile);
            Menu menu = GetActiveMenu(htmlHelper, menuConfig);
            return (menu != null);
        }

        public static PanelBarBuilder LeftMenu(this HtmlHelper htmlHelper, string name, string menuConfigFile)
        {
            LeftMenuConfiguration menuConfig = GetLeftMenuConfiguration(menuConfigFile);
            PanelBarBuilder builder = htmlHelper.Widget().PanelBar().Name(name);
            Menu menu = GetActiveMenu(htmlHelper, menuConfig);
            if (menu != null && menu.Items != null)
            {
                MenuItem activeMenuItem = GetActiveMenuItem(htmlHelper, menuConfig);
                builder.Items(x =>
                {
                    foreach (MenuItem menuItem in menu.Items)
                        BuildPanelBarItem(x.Add(), menuItem, activeMenuItem);
                });
            }
            return builder;
        }

        private static bool BuildPanelBarItem(PanelBarItemBuilder itemBuilder, MenuItem menuItem, MenuItem activeMenuItem)
        {
            bool isSelected = (menuItem == activeMenuItem);
            itemBuilder.Text(menuItem.Title);
            itemBuilder.Action(menuItem.Action, menuItem.Controller);
            if (isSelected)
                itemBuilder.Selected(true);

            bool isExpanded = false;
            if (menuItem.Items != null)
            {
                itemBuilder.Items(x =>
                {
                    foreach (MenuItem childMenuItem in menuItem.Items)
                    {
                        if (BuildPanelBarItem(x.Add(), childMenuItem, activeMenuItem))
                            isExpanded = true;
                    }
                        
                });
            }
            if (isExpanded)
                itemBuilder.Expanded(true);

            return isSelected || isExpanded;
        }
        private static LeftMenuConfiguration GetLeftMenuConfiguration(string menuConfigFile)
        {
            DateTime dt = File.GetLastAccessTime(menuConfigFile);
            if (dt > _leftMenuLastUpdateDate || _leftMenuConfiguration == null)
            {
                MenuConfiguration menuConfig = MenuConfiguration.Load(menuConfigFile);

                var indexedMenuItems = new Dictionary<string, IndexedMenuItem>();
                foreach (Menu menu in menuConfig.Menus)
                {
                    BuildMenuIndex(indexedMenuItems, menu);
                }

                _leftMenuConfiguration = new LeftMenuConfiguration()
                {
                    MenuConfiguration = menuConfig,
                    IndexedMenuItems = indexedMenuItems
                };
                _leftMenuLastUpdateDate = dt;
            }

            return _leftMenuConfiguration;
        }

        private static void BuildMenuIndex(IDictionary<string, IndexedMenuItem> indexedMenuItems, Menu parentMenu)
        {
            foreach (MenuItem menuItem in parentMenu.Items)
            {
                IEnumerable<string> miKeys = GetIndexedMenuItemKeys(menuItem);
                var imi = new IndexedMenuItem() {ParentMenu = parentMenu, Item = menuItem};
                BuildMenuIndex(indexedMenuItems, imi);
            }
        }
        private static void BuildMenuIndex(IDictionary<string, IndexedMenuItem> indexedMenuItems, IndexedMenuItem indexedMenuItem)
        {
            IEnumerable<string> miKeys = GetIndexedMenuItemKeys(indexedMenuItem.Item);
            foreach (string miKey in miKeys)
            {
                indexedMenuItems.Add(miKey, indexedMenuItem);
            }

            if (indexedMenuItem.Item.Items == null)
                return;

            foreach (MenuItem childMenuItem in indexedMenuItem.Item.Items)
            {
                var imi = new IndexedMenuItem() { ParentMenuItem = indexedMenuItem, Item = childMenuItem };   
                BuildMenuIndex(indexedMenuItems, imi);
            }
        }
        private static IEnumerable<string> GetIndexedMenuItemKeys(MenuItem menuItem)
        {
            if (string.IsNullOrEmpty(menuItem.Controller))
                return new string[] {Guid.NewGuid().ToString()};

            var keys = new List<string>() { menuItem.Controller + "_" + menuItem.Action };
            if (menuItem.DependencyActions == null)
                return keys;

            keys.AddRange(menuItem.DependencyActions.Select(depAction => depAction.Controller + "_" + depAction.Action));
            return keys;
        }
        private static string GetActiveIndexedMenuItemKey(HtmlHelper htmlHelper)
        {
            ActionInfo ai = htmlHelper.GetCurrentAction();
            return ai.Controller + "_" + ai.Action;
        }

        private static Menu GetActiveMenu(HtmlHelper htmlHelper, LeftMenuConfiguration menuConfig)
        {
            string key = GetActiveIndexedMenuItemKey(htmlHelper);
            IndexedMenuItem mi;
            if (!menuConfig.IndexedMenuItems.TryGetValue(key, out mi))
                return null;

            while (mi.ParentMenuItem != null)
            {
                mi = mi.ParentMenuItem;
            }
            return mi.ParentMenu;
        }
        private static MenuItem GetActiveMenuItem(HtmlHelper htmlHelper, LeftMenuConfiguration menuConfig)
        {
            string key = GetActiveIndexedMenuItemKey(htmlHelper);
            IndexedMenuItem mi;
            if (menuConfig.IndexedMenuItems.TryGetValue(key, out mi))
                return mi.Item;

            return null;
        }

    }
}
