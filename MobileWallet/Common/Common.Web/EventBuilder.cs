using System.Collections.Generic;

namespace Common.Web
{
    public class EventBuilder
    {
        private readonly IDictionary<string, string> _events;

        protected EventBuilder(IDictionary<string, string> events)
        {
            _events = events;
        }

        protected void AddHandler(string name, string handler)
        {
            _events[name] = handler;
        }
    }
}
