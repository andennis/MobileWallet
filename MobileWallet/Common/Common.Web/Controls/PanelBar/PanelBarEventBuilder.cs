using System.Collections.Generic;

namespace Common.Web.Controls.PanelBar
{
    public class PanelBarEventBuilder : EventBuilder
    {
        public PanelBarEventBuilder(IDictionary<string, string> events)
            : base(events)
        {
        }

        public PanelBarEventBuilder Expand(string handler)
        {
            AddHandler(PanelBar.EventExpand, handler);
            return this;
        }
        public PanelBarEventBuilder ContentLoad(string handler)
        {
            AddHandler(PanelBar.EventContentLoad, handler);
            return this;
        }
        public PanelBarEventBuilder Activate(string handler)
        {
            AddHandler(PanelBar.EventActivate, handler);
            return this;
        }
        public PanelBarEventBuilder Collapse(string handler)
        {
            AddHandler(PanelBar.EventCollapse, handler);
            return this;
        }
        public PanelBarEventBuilder Select(string handler)
        {
            AddHandler(PanelBar.EventSelect, handler);
            return this;
        }
        public PanelBarEventBuilder Error(string handler)
        {
            AddHandler(PanelBar.EventError, handler);
            return this;
        }

    }
}
