using System;

namespace Common.Web.Popup
{
    public class WindowBuilder : WidgetBuilderBase<Window, WindowBuilder>
    {
        private WindowEventBuilder _eventBuilder;
        private WindowResizingSettingsBuilder _resizingSettingsBuilder;
        private WindowActionsBuilder _actionsBuilder;
        private WindowPositionSettingsBuilder _positionSettingsBuilder;

        public WindowBuilder(Window component)
            :base(component)
        {
        }

        public WindowEventBuilder EventBuilder
        {
            get { return _eventBuilder ?? (_eventBuilder = new WindowEventBuilder(_component.Events)); }
        }
        public WindowResizingSettingsBuilder ResizingSettingsBuilder
        {
            get { return _resizingSettingsBuilder ?? (_resizingSettingsBuilder = new WindowResizingSettingsBuilder(_component.ResizingSettings)); }
        }
        public WindowActionsBuilder ActionsBuilder
        {
            get { return _actionsBuilder ?? (_actionsBuilder = new WindowActionsBuilder(_component.Actions)); }
        }
        public WindowPositionSettingsBuilder PositionSettingsBuilder
        {
            get { return _positionSettingsBuilder ?? (_positionSettingsBuilder = new WindowPositionSettingsBuilder(_component.PositionSettings)); }
        }

        /*
        public WindowBuilder Title(bool show)
        {
            _component.TitleVisible = show;
            return this;
        }
        */

        public WindowBuilder Title(string title)
        {
            _component.Title = title;
            return this;
        }

        /*
        public WindowBuilder LoadContentFrom(string actionName, string controllerName, object routeValues)
        {
            return this;
        }

        public WindowBuilder LoadContentFrom(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return this;
        }
        */

        public WindowBuilder LoadContentFrom(string url)
        {
            _component.ContentUrl = url;
            return this;
        }

        public WindowBuilder Events(Action<WindowEventBuilder> clientEventsAction)
        {
            clientEventsAction(this.EventBuilder);
            return this;
        }

        public WindowBuilder Resizable(Action<WindowResizingSettingsBuilder> resizingSettingsAction)
        {
            resizingSettingsAction(this.ResizingSettingsBuilder);
            return this;
        }

        public WindowBuilder Actions(Action<WindowActionsBuilder> actionsBuilderAction)
        {
            actionsBuilderAction(this.ActionsBuilder);
            return this;
        }

        public WindowBuilder Width(int width)
        {
            _component.Width = width;
            return this;
        }

        public WindowBuilder Height(int height)
        {
            _component.Height = height;
            return this;
        }

        public WindowBuilder Position(Action<WindowPositionSettingsBuilder> positionSettingsAction)
        {
            positionSettingsAction(this.PositionSettingsBuilder);
            return this;
        }

        public WindowBuilder Visible(bool visible)
        {
            _component.Visible = visible;
            return this;
        }
        public WindowBuilder Modal(bool modal)
        {
            _component.Modal = modal;
            return this;
        }
        public WindowBuilder Resizable(bool value)
        {
            _component.Resizable = value;
            return this;
        }
        public WindowBuilder Draggable(bool value)
        {
            _component.Draggable = value;
            return this;
        }
        public WindowBuilder Pinned(bool value)
        {
            _component.Pinned = value;
            return this;
        }
        public WindowBuilder AutoFocus(bool value)
        {
            _component.AutoFocus = value;
            return this;
        }
        public WindowBuilder Iframe(bool value)
        {
            _component.Iframe = value;
            return this;
        }
        public WindowBuilder AppendTo(string selector)
        {
            _component.AppendTo = selector;
            return this;
        }

    }
}
