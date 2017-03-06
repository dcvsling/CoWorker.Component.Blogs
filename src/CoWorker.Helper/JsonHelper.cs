namespace Newtonsoft.Json
{
    using Newtonsoft.Json;
    public static class JsonHelper
    {
        public static string ToJson(this object obj)
            => JsonConvert.SerializeObject(obj);

        public static T ToObject<T>(this string json)
            => JsonConvert.DeserializeObject<T>(json);

        public static T Bind<T>(this string json, T t)
        {
            JsonConvert.PopulateObject(json, t);
            return t;
        }
    }
}
