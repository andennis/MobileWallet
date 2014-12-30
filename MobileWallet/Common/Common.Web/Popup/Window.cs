using System.IO;
using System.Linq;
using System.Web.UI;
using Common.Extensions;
using Common.Extensions.JsonNetConverters;
using Newtonsoft.Json;

namespace Common.Web.Popup
{
    public class Window : WidgetBase
    {
        public const string EventOpen = "open";
        public const string EventActivate = "activate";
        public const string EventDeactivate = "deactivate";
        public const string EventClose = "close";
        public const string EventDragstart = "dragstart";
        public const string EventDragend = "dragend";
        public const string EventResize = "resize";
        public const string EventRefresh = "refresh";
        public const string EventError = "error";
        public const string EventDataHandler = "datahandler";

        private WindowPositionSettings _positionSettings;
        private WindowResizingSettings _resizingSettings;
        private WindowButtons _actions;

        public Window()
        {
            //TitleVisible = true;
            Draggable = true;
            Modal = true;
            AutoFocus = true;
            Iframe = true;
        }

        //public bool TitleVisible { get; set; }
        public string Title { get; set; }
        public string ContentUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Visible { get; set; }
        public bool Resizable { get; set; }
        public bool Draggable { get; set; }
        public bool Modal { get; set; }
        public bool Pinned { get; set; }
        public bool AutoFocus { get; set; }
        public bool Iframe { get; set; }
        public string AppendTo { get; set; }

        public WindowPositionSettings PositionSettings
        {
            get { return _positionSettings ?? (_positionSettings = new WindowPositionSettings()); }
        }
        public WindowResizingSettings ResizingSettings
        {
            get { return _resizingSettings ?? (_resizingSettings = new WindowResizingSettings()); }
        }
        public WindowButtons Actions
        {
            get { return _actions ?? (_actions = new WindowButtons()); }
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, Name);
            if (!string.IsNullOrEmpty(ContentUrl))
                writer.AddAttribute("data-popup-action", ContentUrl);

            string dataHandler;
            if (Events.TryGetValue(EventDataHandler, out dataHandler))
                writer.AddAttribute("data-popup-datahandler", dataHandler);

            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();
        }

        private class WindowSettings
        {
            public string title { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public bool visible { get; set; }
            public bool draggable { get; set; }
            public bool modal { get; set; }
            public bool pinned { get; set; }
            public bool autoFocus { get; set; }
            public bool iframe { get; set; }
            public string appendTo { get; set; }
            public bool resizable { get; set; }

            public WindowPositionSettings position { get; set; }

            public int? maxWidth { get; set; }
            public int? maxHeight { get; set; }
            public int? minWidth { get; set; }
            public int? minHeight { get; set; }

            public string[] actions { get; set; }

            //events
            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string open { get; set; }

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string activate { get; set; }

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string deactivate { get; set; }

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string close { get; set; }

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string dragstart { get; set; }

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string dragend { get; set; }

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string resize { get; set; }

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string refresh { get; set; }

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string error { get; set; }
        }

        protected override void WriteInitializationScript(TextWriter writer)
        {
            //string customOpenHandler;
            //Events.TryGetValue(EventOpen, out customOpenHandler);

            var settings = new WindowSettings
            {
                title = Title,
                width = Width,
                height = Height,
                visible = Visible,
                draggable = Draggable,
                modal = Modal,
                pinned = Pinned,
                autoFocus = AutoFocus,
                iframe = Iframe,
                appendTo = AppendTo,
                resizable = _resizingSettings != null ? _resizingSettings.Enabled : Resizable,
                position = _positionSettings,
                maxWidth = _resizingSettings != null ? _resizingSettings.MaxWidth : null,
                maxHeight = _resizingSettings != null ? _resizingSettings.MaxHeight : null,
                minWidth = _resizingSettings != null ? _resizingSettings.MinWidth : null,
                minHeight = _resizingSettings != null ? _resizingSettings.MinHeight : null,
                actions = _actions != null ? _actions.Container.Select(x => x.ToString()).ToArray() : null,

                //open = GetOpenEvent(ContentUrl, null, customOpenHandler),
                open = Events.ContainsKey(EventOpen) ? Events[EventOpen] : null,
                activate = Events.ContainsKey(EventActivate) ? Events[EventActivate] : null,
                deactivate = Events.ContainsKey(EventDeactivate) ? Events[EventDeactivate] : null,
                close = Events.ContainsKey(EventClose) ? Events[EventClose] : null,
                dragstart = Events.ContainsKey(EventDragstart) ? Events[EventDragstart] : null,
                dragend = Events.ContainsKey(EventDragend) ? Events[EventDragend] : null,
                resize = Events.ContainsKey(EventResize) ? Events[EventResize] : null,
                refresh = Events.ContainsKey(EventRefresh) ? Events[EventRefresh] : null,
                error = Events.ContainsKey(EventError) ? Events[EventError] : null,
            };

            string jsonSettings = settings.ObjectToJson();
            string widgetScript = string.Format("$(\"#{0}\").kendoWindow({1})", Name, jsonSettings);
            string script = GetDocumentReadyScript(widgetScript);
            writer.WriteLine(script);
        }

        protected string GetOpenEvent(string contentUrl, object data, string customOpenHandler)
        {
            if (!string.IsNullOrEmpty(contentUrl))
            {
                var options = new {url = contentUrl, data};
                if (!string.IsNullOrEmpty(customOpenHandler))
                    customOpenHandler += "(this);";
                else
                    customOpenHandler = string.Empty;

                return string.Format(@"function(){{
                    this.refresh({0});
                    {1}  
                }}", options.ObjectToJson(), customOpenHandler);
            }

            return customOpenHandler;
        }
    }
}
