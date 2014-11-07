
namespace Common.Web.Popup
{
    public class WindowResizingSettingsBuilder
    {
        private readonly WindowResizingSettings _settings;

        public WindowResizingSettingsBuilder(WindowResizingSettings settings)
        {
            _settings = settings;
        }

        public WindowResizingSettingsBuilder Enabled(bool enable)
        {
            _settings.Enabled = enable;
            return this;
        }

        public WindowResizingSettingsBuilder MinWidth(int minWidth)
        {
            _settings.MinWidth = minWidth;
            return this;
        }

        public WindowResizingSettingsBuilder MaxWidth(int maxWidth)
        {
            _settings.MaxWidth = maxWidth;
            return this;
        }

        public WindowResizingSettingsBuilder MinHeight(int minHeight)
        {
            _settings.MinHeight = minHeight;
            return this;
        }

        public WindowResizingSettingsBuilder MaxHeight(int maxHeight)
        {
            _settings.MaxHeight = maxHeight;
            return this;
        }

    }
}
