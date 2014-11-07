
namespace Common.Web.Popup
{
    public class WindowResizingSettings
    {
        public WindowResizingSettings()
        {
            Enabled = true;
        }

        public bool Enabled { get; set; }
        public int? MinWidth { get; set; }
        public int? MinHeight { get; set; }
        public int? MaxWidth { get; set; }
        public int? MaxHeight { get; set; }
    }
}
