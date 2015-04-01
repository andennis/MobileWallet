using System;
using System.Collections.Generic;

namespace Common.Web.DatePicker
{
    public class DatePickerBuilder : WidgetBuilderBase<DatePicker, DatePickerBuilder>
    {
        private MonthTemplateBuilder _monthTemplateBuilder;
        public DatePickerBuilder(DatePicker component)
            :base(component)
        {
            _monthTemplateBuilder = new MonthTemplateBuilder(component.MonthTemplate);
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
            monthTemplateAction(_monthTemplateBuilder);
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

    }
}
