using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Extensions
{
    public static class ObjectExtensions
    {
        public static IDictionary<string, object> ObjectPropertiesToDictionary(this object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            var dict = new Dictionary<string, object>();
            PropertyInfo[] properties = obj.GetType().GetProperties();

            return properties.ToDictionary(key => key.Name, val => val.GetValue(obj));
        }
    }
}
