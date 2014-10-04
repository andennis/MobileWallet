using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Common.Extensions;

namespace Common.Web
{
    public class SelectTListTyped<TObject, TDataField, TTextField> : SelectList
    {
        public SelectTListTyped(IEnumerable<TObject> items, Expression<Func<TObject, TDataField>> dataValueField, Expression<Func<TObject, TTextField>> dataTextField)
            :base(items, dataValueField.GetMethodOrPropertyName(), dataTextField.GetMethodOrPropertyName())
        {
        }
    }
}
