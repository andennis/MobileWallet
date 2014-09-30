using System.Collections.Generic;

namespace Common.Web.Tab
{
    public class TabStripEventBuilder
    {
        private readonly IDictionary<string, object> _events;

        public TabStripEventBuilder(IDictionary<string, object> events)
        {
            _events = events;
        }

        public TabStripEventBuilder Show(string handler)
        {
            _events.Add("show", handler);
            return this;
        }
        public TabStripEventBuilder Activate(string handler)
        {
            _events.Add("activate", handler);
            return this;
        }
        public TabStripEventBuilder Select(string handler)
        {
            _events.Add("select", handler);
            return this;
        }
        public TabStripEventBuilder Error(string handler)
        {
            _events.Add("error", handler);
            return this;
        }
    }
}
