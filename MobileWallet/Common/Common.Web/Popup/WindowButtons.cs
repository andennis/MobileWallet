using System.Collections.Generic;

namespace Common.Web.Popup
{
    public class WindowButtons
    {
        private readonly IList<WindowButton> _container = new List<WindowButton>(){WindowButton.Close};
        public IList<WindowButton> Container { get { return _container; } }
    }
}
