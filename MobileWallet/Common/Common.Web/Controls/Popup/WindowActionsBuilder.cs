namespace Common.Web.Controls.Popup
{
    public class WindowActionsBuilder
    {
        private readonly WindowButtons _buttons;

        public WindowActionsBuilder(WindowButtons buttons)
        {
            _buttons = buttons;
        }

        public WindowActionsBuilder Close()
        {
            AddButton(WindowButton.Close);
            return this;
        }

        public WindowActionsBuilder Maximize()
        {
            AddButton(WindowButton.Maximize);
            return this;
        }

        public WindowActionsBuilder Minimize()
        {
            AddButton(WindowButton.Minimize);
            return this;
        }

        public WindowActionsBuilder Pin()
        {
            AddButton(WindowButton.Pin);
            return this;
        }

        public WindowActionsBuilder Refresh()
        {
            AddButton(WindowButton.Refresh);
            return this;
        }

        public WindowActionsBuilder Clear()
        {
            AddButton(WindowButton.Clear);
            return this;
        }

        private void AddButton(WindowButton button)
        {
            if (_buttons.Container.Contains(button))
                _buttons.Container.Add(button);
        }
    }
}
