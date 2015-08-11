using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Common.Web
{
    public static class EnumHelper
    {
        public static IEnumerable<SelectListItem> ToSelectListItems<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(x => new SelectListItem
                {
                    Text = x.ToString(),
                    Value = Convert.ToInt32(x).ToString()
                });
        }

        public static SelectList ToSelectList<TEnum>() where TEnum : struct
        {
            IEnumerable<SelectListItem> items = ToSelectListItems<TEnum>();
            return new SelectList(items, "Value", "Text");
        }

        public static IDictionary<string, int> ToDictionary<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(k => k.ToString(), v => Convert.ToInt32(v));
        }

        /*
        public static IEnumerable<TEnum> ToEmunArray<TEnum>(string commaSeparatedIds) where TEnum : struct
        {
            IEnumerable<int> enumIds = commaSeparatedIds.Split(',').Select(x => Convert.ToInt32(x));
            return ToEmunArray<TEnum>(enumIds);
        }
        public static IEnumerable<TEnum> ToEmunArray<TEnum>(IEnumerable<int> enumIds) where TEnum : struct
        {
            return enumIds.Select(x => (TEnum)(object)x);
        }
        */

    }
}
