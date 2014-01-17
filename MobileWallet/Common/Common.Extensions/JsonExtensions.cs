using Newtonsoft.Json;

namespace Common.Extensions
{
    public static class JsonExtensions
    {
        public static string ObjectToJson(this object obj)
        {
            if (obj == null)
                return "{}";

            return JsonConvert.SerializeObject(obj);
        }

        public static TResult JsonToObject<TResult>(this string json)
        {
            return JsonConvert.DeserializeObject<TResult>(json);
        }
    }
}
