using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Common.Extensions;

namespace Common.Web
{
    public class SelectListTyped<TObject, TDataField, TTextField> : SelectList
    {
        public SelectListTyped(IEnumerable<TObject> items, Expression<Func<TObject, TDataField>> dataValueField, Expression<Func<TObject, TTextField>> dataTextField)
            :base(items, dataValueField.GetMethodOrPropertyName(), dataTextField.GetMethodOrPropertyName())
        {
        }
    }
}
