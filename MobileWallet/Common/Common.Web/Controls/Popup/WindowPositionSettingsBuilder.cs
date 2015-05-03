namespace Common.Web.Controls.Popup
{
    public class WindowPositionSettingsBuilder
    {
        private readonly WindowPositionSettings _settings;

        public WindowPositionSettingsBuilder(WindowPositionSettings settings)
        {
            _settings = settings;
        }

        public WindowPositionSettingsBuilder Top(int top)
        {
            _settings.Top = top;
            return this;
        }
        public WindowPositionSettingsBuilder Left(int left)
        {
            _settings.Left = left;
            return this;
        }

    }
}
