namespace System
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Text;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging.Abstractions.Internal;
    using System.Reflection;

    public static class StringHelper
    {
        public static bool IsEmpty(this string str) => string.IsNullOrWhiteSpace(str);

        public static string ToJoin(this IEnumerable<string> iterator,string split) => string.Join(split,iterator.ToArray());

        public static string ReplaceAllToOne(this string str, string newstr, params string[] pattern)
            => pattern.Aggregate(str, (seed, ptn) => seed.Replace(ptn, newstr));

        public static string ReplaceWithRegex(this string str, string newstr, string pattern, RegexOptions options = RegexOptions.None)
            => Regex.Replace(str, pattern, newstr, options);

        public static string MergeToString(this IEnumerable<char> iterator)
            => new string(iterator.ToArray());

        public static string ToUTF8(this byte[] bytes)
            => Encoding.UTF8.GetString(bytes);

        public static byte[] ToBytes(this string str)
            => Encoding.UTF8.GetBytes(str);

        public static string ToJson(this object obj)
            => JsonConvert.SerializeObject(obj);

        public static T ToObject<T>(this string str)
            => JsonConvert.DeserializeObject<T>(str);

        public static string ToFormatString(this Type type)
            => TypeNameHelper.GetTypeDisplayName(type);

        public static string ToFormatString(this TypeInfo type)
            => TypeNameHelper.GetTypeDisplayName(type.AsType());
    }
}
