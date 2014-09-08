using Newtonsoft.Json;

namespace Common.Extensions
{
    public static class JsonExtensions
    {
        private readonly static JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
                                                                             {
                                                                                 NullValueHandling = NullValueHandling.Ignore, 
                                                                                 Formatting = Formatting.Indented
                                                                             };

        public static string ObjectToJson(this object obj)
        {
            if (obj == null)
                return "{}";

            return JsonConvert.SerializeObject(obj, _serializerSettings);
        }

        public static TResult JsonToObject<TResult>(this string json)
        {
            return JsonConvert.DeserializeObject<TResult>(json);
        }
    }
}
