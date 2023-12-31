﻿using System;
using System.Collections;
using Common.Web.Controls.DatePicker;
using Common.Web.Controls.DropDownList;

namespace Common.Web.Controls
{
    public class ListBuilderBase<TDropDown, TDropDownBuilder> : WidgetBuilderBase<TDropDown, TDropDownBuilder>
        where TDropDown : ListBase
        where TDropDownBuilder : ListBuilderBase<TDropDown, TDropDownBuilder>
    {
        private ReadOnlyDataSourceBuilder _dataSourceBuilder;

        public ListBuilderBase(TDropDown component)
            :base(component)
        {
        }

        private ReadOnlyDataSourceBuilder DataSourceBuilder{ get { return _dataSourceBuilder ?? (_dataSourceBuilder = new ReadOnlyDataSourceBuilder()); }}

        public TDropDownBuilder Animation(bool enable)
        {
            _component.Animation.Enabled = enable;
            return (TDropDownBuilder)this;
        }
        public TDropDownBuilder Animation(Action<PopupAnimationBuilder> animationAction)
        {
            throw new NotImplementedException();
            //animationAction(PopupAnimationBuilder);
            //return this;
        }

        public TDropDownBuilder BindTo(IEnumerable data)
        {
            throw new NotImplementedException();
            //return (TDropDownBuilder)this;
        }

        public TDropDownBuilder DataTextField(string field)
        {
            _component.DataTextField = field;
            return (TDropDownBuilder)this;
        }

        public TDropDownBuilder DataSource(Action<ReadOnlyDataSourceBuilder> configurator)
        {
            configurator(DataSourceBuilder);
           return (TDropDownBuilder)this;
        }

        public TDropDownBuilder Delay(int delay)
        {
            _component.Delay = delay;
            return (TDropDownBuilder)this;
        }

        public TDropDownBuilder Enable(bool value)
        {
            _component.Enabled = value;
            return (TDropDownBuilder)this;
        }

        public TDropDownBuilder IgnoreCase(bool ignoreCase)
        {
            _component.IgnoreCase = ignoreCase;
            return (TDropDownBuilder)this;
        }

        public TDropDownBuilder Height(int height)
        {
            _component.Height = height;
            return (TDropDownBuilder)this;
        }

        public TDropDownBuilder HeaderTemplate(string headerTemplate)
        {
            _component.HeaderTemplate = headerTemplate;
            return (TDropDownBuilder)this;
        }

        public TDropDownBuilder HeaderTemplateId(string headerTemplateId)
        {
            _component.HeaderTemplateId = headerTemplateId;
            return (TDropDownBuilder)this;
        }

        public TDropDownBuilder ValuePrimitive(bool valuePrimitive)
        {
            _component.ValuePrimitive = valuePrimitive;
            return (TDropDownBuilder)this;
        }

    }
}
