using System;
using Newtonsoft.Json;

namespace Common.Extensions.JsonNetConverters
{
    public class DateTimeToJavaScriptDateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof (DateTime) || objectType == typeof (DateTime?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime dt = value is DateTime ? (DateTime)value : ((DateTime?) value).Value;
            writer.WriteRawValue(string.Format("new Date({0}, {1}, {2})", dt.Year, dt.Month, dt.Day));
        }
    }
}
