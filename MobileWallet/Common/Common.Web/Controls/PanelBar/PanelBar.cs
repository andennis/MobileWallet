using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Common.Extensions;

namespace Common.Web.Controls.PanelBar
{
    public class PanelBar : WidgetBase, INavigationItemContainer<PanelBarItem>
    {
        public const string EventExpand = "expand";
        public const string EventContentLoad = "contentLoad";
        public const string EventActivate = "activate";
        public const string EventCollapse = "collapse";
        public const string EventSelect = "select";
        public const string EventError = "error";

        private readonly IList<PanelBarItem> _items;
        private ExpandableAnimation _animation;

        public PanelBar(ViewContext viewContext)
            : base(viewContext)
        {
            _items = new List<PanelBarItem>();
            this.SecurityTrimming = new SecurityTrimming();
            this.ExpandMode = PanelBarExpandMode.Multiple;
        }

        protected override void WriteInitializationScript(TextWriter writer)
        {
            var settings = new
            {
                expandMode = this.ExpandMode.ToString().ToLower(),
                animation = _animation
                //dataSource = GetDataSource()
            };

            string jsonSettings = settings.ObjectToJson();
            string widgetScript = string.Format("$(\"#{0}\").kendoPanelBar({1})", Name, jsonSettings);
            string script = GetDocumentReadyScript(widgetScript);
            writer.WriteLine(script);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, Name);
            WriteItems(writer, _items);
        }

        /*
        private IEnumerable GetDataSource()
        {
            var ds = new List<DataSourceItem>();
            BuildDataSource(ds, _items);
            return ds;
        }

        private class DataSourceItem
        {
            public string text { get; set; }
            public string cssClass { get; set; }
            public string url { get; set; }
            public bool? encoded { get; set; }
            //public string content { get; set; }
            public string contentUrl { get; set; }
            public string imageUrl { get; set; }
            public bool? expanded { get; set; }
            public IList<DataSourceItem> items { get; set; }
            public bool? active { get; set; }
        }

        private void BuildDataSource(IList<DataSourceItem> dataSource, IEnumerable<PanelBarItem> panelItems)
        {
            if (!panelItems.Any())
                return;

            foreach (PanelBarItem pnlItem in panelItems)
            {
                var dsItem = new DataSourceItem()
                {
                    text = pnlItem.Text,
                    cssClass = pnlItem.SpriteCssClasses, //???
                    url = pnlItem.Url,
                    encoded = pnlItem.Encoded ? pnlItem.Encoded : (bool?)null,
                    //content = pnlItem.Content()
                    contentUrl = pnlItem.ContentUrl,
                    imageUrl = pnlItem.ImageUrl,
                    expanded = this.ExpandAll ? true : (pnlItem.Expanded ? pnlItem.Expanded : (bool?)null),
                    active = pnlItem.Selected ? true : (bool?)null
                };
                dataSource.Add(dsItem);

                if (pnlItem.Items.Any())
                {
                    dsItem.items = new List<DataSourceItem>();
                    BuildDataSource(dsItem.items, pnlItem.Items);
                }
                
            }
        }
        */

        private void WriteItems(HtmlTextWriter writer, IEnumerable<PanelBarItem> panelItems)
        {
            if (!panelItems.Any())
                return;

            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            foreach (PanelBarItem item in panelItems)
            {
                if (item.Expanded)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "k-state-active");
                if (!item.Enabled)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "k-state-disabled");

                writer.RenderBeginTag(HtmlTextWriterTag.Li);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "k-link" + (item.Selected ? " k-state-selected" : string.Empty));
                string url = GetItemUrl(item);
                if (!string.IsNullOrEmpty(url))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, url);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                }
                else
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                }

                if (!string.IsNullOrEmpty(item.ImageUrl))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "k-image");
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, item.ImageUrl);
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag();
                }

                writer.WriteEncodedText(item.Text);

                writer.RenderEndTag();

                WriteItems(writer, item.Items);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
        }

        private string GetItemUrl(PanelBarItem item)
        {
            if (!string.IsNullOrEmpty(item.Url))
                return item.Url;

            if (!string.IsNullOrEmpty(item.ControllerName))
            {
                var urlHelper = new UrlHelper(_viewContext.RequestContext);
                return urlHelper.Action(item.ActionName, item.ControllerName, item.RouteValues);
            }

            return null;
        }

        public ExpandableAnimation Animation { get { return _animation ?? (_animation = new ExpandableAnimation()); } }
        public Action<PanelBarItem> ItemAction { get; set; }
        public bool HighlightPath { get; set; }
        public PanelBarExpandMode ExpandMode { get; set; }
        public bool ExpandAll { get; set; }
        public int SelectedIndex { get; set; }
        public Effects Effects { get; set; }
        public IList<PanelBarItem> Items
        {
            get { return _items; }
        }
        public SecurityTrimming SecurityTrimming { get; set; }

    }
}
