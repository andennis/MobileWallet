﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using Common.Extensions;
using Common.Extensions.JsonNetConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Web.Popup
{
    public class Window : WidgetBase
    {
        public const string EventOpen = "open";

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
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();
        }

        protected override void WriteInitializationScript(TextWriter writer)
        {
            var settings = new
            {
                title = Title,
                content = ContentUrl,
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
                position = _positionSettings != null ? new {top = _positionSettings.Top, left = _positionSettings.Left} : null,
                maxWidth = _resizingSettings != null ? _resizingSettings.MaxWidth : null,
                maxHeight = _resizingSettings != null ? _resizingSettings.MaxHeight : null,
                minWidth = _resizingSettings != null ? _resizingSettings.MinWidth : null,
                minHeight = _resizingSettings != null ? _resizingSettings.MinHeight : null,
                actions = _actions != null ? _actions.Container.Select(x => x.ToString()).ToArray() : null,
            };

            IDictionary<string, object> events = this.Events
                .Where(x => x.Key != EventOpen)
                //.ToDictionary(k => k.Key, v => (object)(new JsonValueWithoutQuotes(v.Value)));
                .ToDictionary(k => k.Key, v => (object)v.Value);

            string customOpenHandler;
            this.Events.TryGetValue(EventOpen, out customOpenHandler);
            string openEvent = GetOpenEvent(ContentUrl, null, customOpenHandler);
            if (!string.IsNullOrEmpty(openEvent))
                //events.Add(EventOpen, new JsonValueWithoutQuotes(openEvent));
                events.Add(EventOpen, openEvent);

            string jsonEvents = events.DictionaryToJsonAsObject();
            string jsonSettings = settings.ObjectToJson();
            jsonSettings = jsonSettings.MergeJson(jsonEvents);

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
                    customOpenHandler += "();";
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
