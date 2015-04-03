using System;
using System.Collections.Generic;

namespace Common.Web.DatePicker
{
    public class DatePickerBuilder : WidgetBuilderBase<DatePicker, DatePickerBuilder>
    {
        private MonthTemplateBuilder _monthTemplateBuilder;
        private DatePickerEventBuilder _eventBuilder;
        private PopupAnimationBuilder _popupAnimationBuilder;

        public DatePickerBuilder(DatePicker component)
            :base(component)
        {
        }

        private DatePickerEventBuilder EventBuilder
        {
            get { return _eventBuilder ?? (_eventBuilder = new DatePickerEventBuilder(_component.Events)); }
        }
        private MonthTemplateBuilder MonthTemplateBuilder
        {
            get { return _monthTemplateBuilder ?? (_monthTemplateBuilder = new MonthTemplateBuilder(_component.MonthTemplate)); }
        }
        private PopupAnimationBuilder PopupAnimationBuilder
        {
            get { return _popupAnimationBuilder ?? (_popupAnimationBuilder = new PopupAnimationBuilder(_component.Animation)); }
        }

        
        public DatePickerBuilder ARIATemplate(string template)
        {
            _component.ARIATemplate = template;
            return this;
        }
        
        public DatePickerBuilder BindTo(List<DateTime> dates)
        {
            throw new NotImplementedException();
            //return this;
        }

        public DatePickerBuilder FooterId(string id)
        {
            _component.FooterId = id;
            return this;
        }

        public DatePickerBuilder Footer(string footer)
        {
            _component.Footer = footer;
            return this;
        }

        public DatePickerBuilder Footer(bool footer)
        {
            _component.EnableFooter = footer;
            return this;
        }

        public DatePickerBuilder Depth(CalendarView depth)
        {
            _component.Depth = depth.ToString().ToLower();
            return this;
        }

        public DatePickerBuilder Start(CalendarView start)
        {
            _component.Start = start.ToString().ToLower();
            return this;
        }

        public DatePickerBuilder MonthTemplateId(string id)
        {
            _component.MonthTemplate.ContentId = id;
            return this;
        }

        public DatePickerBuilder MonthTemplate(string content)
        {
            _component.MonthTemplate.Content = content;
            return this;
        }

        public DatePickerBuilder MonthTemplate(Action<MonthTemplateBuilder> monthTemplateAction)
        {
            monthTemplateAction(MonthTemplateBuilder);
            return this;
        }

        public DatePickerBuilder Min(DateTime date)
        {
            _component.Min = date.Date;
            return this;
        }

        public DatePickerBuilder Max(DateTime date)
        {
            _component.Max = date.Date;
            return this;
        }

        public DatePickerBuilder Animation(bool enable)
        {
            _component.Animation.Enabled = enable;
            return this;
        }
        public DatePickerBuilder Animation(Action<PopupAnimationBuilder> animationAction)
        {
            animationAction(PopupAnimationBuilder);
            return this;
        }

        public DatePickerBuilder Culture(string culture)
        {
            _component.Culture = culture;
            return this;
        }

        public DatePickerBuilder Events(Action<DatePickerEventBuilder> clientEventsAction)
        {
            clientEventsAction(EventBuilder);
            return this;
        }

        public DatePickerBuilder Format(string format)
        {
            _component.Format = format;
            return this;
        }
        public DatePickerBuilder Enable(bool value)
        {
            _component.Enabled = value;
            return this;
        }

        public DatePickerBuilder Value(DateTime? date)
        {
            _component.Value = date;
            return this;
        }

        /*
        public DatePickerBuilder ParseFormats(IEnumerable<string> formats)
        {
            return this;
        }
        public DatePickerBuilder Value(string date)
        {
            return this;
        }
        */


    }
}
