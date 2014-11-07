using System.IO;
using System.Linq;
using System.Web.UI;
using Common.Extensions;

namespace Common.Web.Popup
{
    public class Window : WidgetBase
    {
        private WindowPositionSettings _positionSettings;
        private WindowResizingSettings _resizingSettings;
        private WindowButtons _actions;

        public Window()
        {
            //TitleVisible = true;
            Draggable = true;
            Modal = true;
            AutoFocus = true;
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
                actions = _actions != null ? _actions.Container.Select(x => x.ToString()).ToArray() : null
            };

            string widgetScript = string.Format("$(\"#{0}\").kendoWindow({1})", Name, settings.ObjectToJson());
            string script = GetDocumentReadyScript(widgetScript);
            writer.WriteLine(script);
        }
    }
}
