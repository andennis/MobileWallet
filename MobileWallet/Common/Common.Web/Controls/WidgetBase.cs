using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using Common.Extensions;

namespace Common.Web.Controls
{
    public class WidgetBase
    {
        private readonly IDictionary<string, object> _htmlAttributes = new Dictionary<string, object>();
        protected readonly ViewContext _viewContext;

        protected WidgetBase(ViewContext viewContext)
        {
            _viewContext = viewContext;
            Events = new Dictionary<string, string>();
        }

        public string Id
        {
            get { return Name; }
        }

        public string Name { get; set; }
        public IDictionary<string, object> HtmlAttributes { get { return _htmlAttributes; } }

        public IDictionary<string, string> Events { get; private set; }

        public string Render()
        {
            var writer = new HtmlTextWriter(new StringWriter());

            WriteHtml(writer);
            WriteInitializationScript(writer);

            return writer.InnerWriter.ToString();
        }

        protected virtual void WriteHtml(HtmlTextWriter writer)
        {
        }
        protected virtual void WriteInitializationScript(TextWriter writer)
        {
        }

        public string ToHtmlString()
        {
            return Render();
        }

        public override string ToString()
        {
            return ToHtmlString();
        }

        protected string GetDocumentReadyScript(string widgetInitScript)
        {
            var scriptTag = new TagBuilder("script")
                            {
                                InnerHtml = string.Format(@"$(document).ready(function(){{{0}}})", widgetInitScript)
                            };
            return scriptTag.ToString();
        }
        protected string GetEvent(string eventKey)
        {
            return Events.ContainsKey(eventKey) ? Events[eventKey] : null;
        }

        protected object GetModelValue()
        {
            return _viewContext.ViewData.Model.GetPropertyValue(Name);
        }
    }
}
