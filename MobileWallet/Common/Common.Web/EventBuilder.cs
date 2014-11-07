using System.Collections.Generic;

namespace Common.Web
{
    public class EventBuilder
    {
        private readonly IDictionary<string, object> _events;

        protected EventBuilder(IDictionary<string, object> events)
        {
            _events = events;
        }

        protected void AddHandler(string name, string handler)
        {
            _events[name] = handler;
        }
    }
}
