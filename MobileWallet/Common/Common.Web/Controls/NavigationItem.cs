using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace Common.Web.Controls
{
    public abstract class NavigationItem<T> where T : NavigationItem<T>
    {
        protected NavigationItem()
        {
            HtmlAttributes = new Dictionary<string, object>();
            LinkHtmlAttributes = new Dictionary<string, object>();
            ImageHtmlAttributes = new Dictionary<string, object>();
            ContentHtmlAttributes = new Dictionary<string, object>();
            this.Enabled = true;
        }

        public RouteValueDictionary RouteValues { get; set; }
        public IDictionary<string, object> HtmlAttributes { get; private set; }
        public IDictionary<string, object> ImageHtmlAttributes { get; private set; }
        public IDictionary<string, object> LinkHtmlAttributes { get; private set; }
        public IDictionary<string, object> ContentHtmlAttributes { get; private set; }

        public bool Encoded { get; set; }
        /*
        [ScriptIgnore]
        public HtmlTemplate Template { get; }
        [ScriptIgnore]
        public string Html { get; set; }
        */
        public bool Visible { get; set; }
        public string ImageUrl { get; set; }
        public string SpriteCssClasses { get; set; }
        public Action Content { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
        public bool Enabled { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        /*
        [ScriptIgnore]
        public string RouteName { get; set; }
        */
        public string Url { get; set; }

    }
}
