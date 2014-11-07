using Common.Web.Popup;

namespace Common.Web
{
    public class WidgetFactory
    {
        public virtual WindowBuilder Window()
        {
            return new WindowBuilder(new Window());
        }
    }
}
