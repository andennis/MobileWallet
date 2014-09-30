using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Common.Extensions;

namespace Common.Web.Tab
{
    public class TabStripBuilder : IHtmlString
    {
        private string _name;
        private int _selectedIndex;

        private readonly TabStripItemFactory _tabStripItemFactory = new TabStripItemFactory();
        private readonly TabStripEventBuilder _tabStripEventBuilder;
        private readonly IDictionary<string, object> _events;

        public TabStripBuilder()
        {
            _events = new Dictionary<string, object>();
            _tabStripEventBuilder = new TabStripEventBuilder(_events);
        }

        public TabStripBuilder Name(string name)
        {
            _name = name;
            return this;
        }

        public TabStripBuilder Items(Action<TabStripItemFactory> addAction)
        {
            addAction(_tabStripItemFactory);
            return this;
        }

        public TabStripBuilder Events(Action<TabStripEventBuilder> clientEventsAction)
        {
            clientEventsAction(_tabStripEventBuilder);
            return this;
        }

        public TabStripBuilder SelectedIndex(int index)
        {
            _selectedIndex = index;
            return this;
        }

        public string ToHtmlString()
        {
            return RenderTabStrip();
        }

        private string RenderTabStrip()
        {
            var sb = new StringBuilder();
            var mainTag = new TagBuilder("div");
            mainTag.GenerateId(_name);
            sb.AppendLine(mainTag.ToString());
            sb.AppendLine(GetInitializationScript());

            return sb.ToString();
        }

        private string GetInitializationScript()
        {
            var dsItems = _tabStripItemFactory.Items.Select(x =>
                new
                {
                    Name = x.Text,
                    Content = x.Content,
                    ContentUrl = x.LoadContentUrl
                });

            var tabStripSettings = new
            {
                dataTextField = "Name",
                dataContentField = "Content",
                dataContentUrlField = "ContentUrl",
                dataSource = dsItems
            };

            var scriptTag = new TagBuilder("script");
            scriptTag.InnerHtml = @"$(document).ready(function () {
                $('#" + _name + @"').kendoTabStrip(" + tabStripSettings.ObjectToJson() + @");
            })";

            return scriptTag.ToString();
        }

    }
}
