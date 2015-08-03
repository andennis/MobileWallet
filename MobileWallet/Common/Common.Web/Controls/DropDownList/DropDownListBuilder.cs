using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Common.Web.Controls.DropDownList
{
    public class DropDownListBuilder : ListBuilderBase<DropDownList, DropDownListBuilder>
    {
        private DropDownListEventBuilder _eventBuilder;
        private DropDownListItemFactory _itemFactory;

        public DropDownListBuilder(DropDownList component)
            : base(component)
        {
        }

        private DropDownListEventBuilder EventBuilder
        {
            get { return _eventBuilder ?? (_eventBuilder = new DropDownListEventBuilder(_component.Events)); }
        }
        private DropDownListItemFactory ItemFactory
        {
            get { return _itemFactory ?? (_itemFactory = new DropDownListItemFactory(_component.Items)); }
        }

        public DropDownListBuilder AutoBind(bool autoBind)
        {
            _component.AutoBind = autoBind;
            return this;
        }

        public DropDownListBuilder  BindTo(IEnumerable<SelectListItem> dataSource)
        {
            if (dataSource == null)
                throw new ArgumentNullException("dataSource");

            foreach (var dataItem in dataSource)
            {
                ItemFactory.Add()
                    .Text(dataItem.Text)
                    .Value(dataItem.Value)
                    .Selected(dataItem.Selected);
            }
            return this;
        }
        public DropDownListBuilder DataValueField(string field)
        {
            _component.DataValueField = field;
            return this;
        }
        public DropDownListBuilder Events(Action<DropDownListEventBuilder> clientEventsAction)
        {
            clientEventsAction(EventBuilder);
            return this;
        }
        public DropDownListBuilder Filter(string filter)
        {
            _component.Filter = filter;
            return this;
        }
        public DropDownListBuilder Filter(FilterType filter)
        {
            _component.Filter = filter.ToString();
            return this;
        }
        public DropDownListBuilder Items(Action<DropDownListItemFactory> addAction)
        {
            addAction(ItemFactory);
            return this;
        }
        public DropDownListBuilder OptionLabel(string optionLabel)
        {
            _component.OptionLabel = optionLabel;
            return this;
        }
        public DropDownListBuilder MinLength(int length)
        {
            _component.MinLength = length;
            return this;
        }
        public DropDownListBuilder SelectedIndex(int index)
        {
            _component.SelectedIndex = index;
            return this;
        }
        public DropDownListBuilder SelectedValue(object value)
        {
            _component.Value = Convert.ToString(value);
            return this;
        }
        public DropDownListBuilder CascadeFrom(string cascadeFrom)
        {
            _component.CascadeFrom = cascadeFrom;
            return this;
        }
        public DropDownListBuilder CascadeFromField(string cascadeFromField)
        {
            _component.CascadeFromField = cascadeFromField;
            return this;
        }
        public DropDownListBuilder Text(string text)
        {
            _component.Text = text;
            return this;
        }
        public DropDownListBuilder ValueTemplate(string valueTemplate)
        {
            _component.ValueTemplate = valueTemplate;
            return this;
        }
        public DropDownListBuilder ValueTemplateId(string valueTemplateId)
        {
            _component.ValueTemplateId = valueTemplateId;
            return this;
        }

    }
}
