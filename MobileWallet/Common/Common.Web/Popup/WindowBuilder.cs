using System;

namespace Common.Web.Popup
{
    public class WindowBuilder : WidgetBuilderBase<Window, WindowBuilder>
    {
        private readonly WindowEventBuilder _eventBuilder;
        private readonly WindowResizingSettingsBuilder _resizingSettingsBuilder;
        private readonly WindowActionsBuilder _actionsBuilder;
        private readonly WindowPositionSettingsBuilder _positionSettingsBuilder;

        public WindowBuilder(Window component)
            :base(component)
        {
            _resizingSettingsBuilder = new WindowResizingSettingsBuilder(component.ResizingSettings);
            _actionsBuilder = new WindowActionsBuilder(component.Actions);
            _positionSettingsBuilder = new WindowPositionSettingsBuilder(component.PositionSettings);
            _eventBuilder = new WindowEventBuilder(component.Events);
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
            clientEventsAction(_eventBuilder);
            return this;
        }

        public WindowBuilder Resizable(Action<WindowResizingSettingsBuilder> resizingSettingsAction)
        {
            resizingSettingsAction(_resizingSettingsBuilder);
            return this;
        }

        public WindowBuilder Actions(Action<WindowActionsBuilder> actionsBuilderAction)
        {
            actionsBuilderAction(_actionsBuilder);
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
            positionSettingsAction(_positionSettingsBuilder);
            return this;
        }

        public WindowBuilder Visible(bool visible)
        {
            _component.Visible = visible;
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
