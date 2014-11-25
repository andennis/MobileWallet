using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;

namespace Common.Web
{
    public class WidgetBase
    {
        protected WidgetBase()
        {
            Events = new Dictionary<string, string>();
        }

        public string Id
        {
            get { return Name; }
        }

        public string Name { get; set; }

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
            var scriptTag = new TagBuilder("script");
            scriptTag.InnerHtml = string.Format(@"$(document).ready(function(){{{0}}})", widgetInitScript);
            return scriptTag.ToString();
        }
    }
}
