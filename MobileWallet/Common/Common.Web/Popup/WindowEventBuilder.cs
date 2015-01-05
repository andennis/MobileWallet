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
            AddHandler(Window.EventActivate, handler);
            return this;
        }
        public WindowEventBuilder Deactivate(string handler)
        {
            AddHandler(Window.EventDeactivate, handler);
            return this;
        }
        public WindowEventBuilder Close(string handler)
        {
            AddHandler(Window.EventClose, handler);
            return this;
        }
        public WindowEventBuilder DragStart(string handler)
        {
            AddHandler(Window.EventDragstart, handler);
            return this;
        }
        public WindowEventBuilder DragEnd(string handler)
        {
            AddHandler(Window.EventDragend, handler);
            return this;
        }
        public WindowEventBuilder Resize(string handler)
        {
            AddHandler(Window.EventResize, handler);
            return this;
        }
        public WindowEventBuilder Refresh(string handler)
        {
            AddHandler(Window.EventRefresh, handler);
            return this;
        }
        public WindowEventBuilder Error(string handler)
        {
            AddHandler(Window.EventError, handler);
            return this;
        }
        public WindowEventBuilder DataHandler(string handler)
        {
            AddHandler(Window.EventDataHandler, handler);
            return this;
        }
        public WindowEventBuilder Success(string handler)
        {
            AddHandler(Window.EventSuccess, handler);
            return this;
        }
    }
}
