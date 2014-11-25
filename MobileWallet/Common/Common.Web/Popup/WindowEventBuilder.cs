using System.Collections.Generic;

namespace Common.Web.Popup
{
    public class WindowEventBuilder : EventBuilder
    {
        public WindowEventBuilder(IDictionary<string, string> events)
            :base(events)
        {
        }

        public WindowEventBuilder Open(string handler)
        {
            AddHandler(Window.EventOpen, handler);
            return this;
        }
        public WindowEventBuilder Activate(string handler)
        {
            AddHandler("activate", handler);
            return this;
        }
        public WindowEventBuilder Deactivate(string handler)
        {
            AddHandler("deactivate", handler);
            return this;
        }
        public WindowEventBuilder Close(string handler)
        {
            AddHandler("close", handler);
            return this;
        }
        public WindowEventBuilder DragStart(string handler)
        {
            AddHandler("dragstart", handler);
            return this;
        }
        public WindowEventBuilder DragEnd(string handler)
        {
            AddHandler("dragend", handler);
            return this;
        }
        public WindowEventBuilder Resize(string handler)
        {
            AddHandler("resize", handler);
            return this;
        }
        public WindowEventBuilder Refresh(string handler)
        {
            AddHandler("refresh", handler);
            return this;
        }
        public WindowEventBuilder Error(string handler)
        {
            AddHandler("error", handler);
            return this;
        }
    }
}
