using System.Collections.Generic;

namespace Common.Web.Controls.DropDownList
{
    public class DropDownListEventBuilder : EventBuilder
    {
        public DropDownListEventBuilder(IDictionary<string, string> events)
            : base(events)
        {
        }

        public DropDownListEventBuilder Select(string handler)
        {
            AddHandler(DropDownList.EventSelect, handler);
            return this;
        }
        public DropDownListEventBuilder Change(string handler)
        {
            AddHandler(DropDownList.EventChange, handler);
            return this;
        }
        public DropDownListEventBuilder DataBound(string handler)
        {
            AddHandler(DropDownList.EventDataBound, handler);
            return this;
        }
        public DropDownListEventBuilder Open(string handler)
        {
            AddHandler(DropDownList.EventOpen, handler);
            return this;
        }
        public DropDownListEventBuilder Close(string handler)
        {
            AddHandler(DropDownList.EventClose, handler);
            return this;
        }
        public DropDownListEventBuilder Cascade(string handler)
        {
            AddHandler(DropDownList.EventCascade, handler);
            return this;
        }

    }
}
