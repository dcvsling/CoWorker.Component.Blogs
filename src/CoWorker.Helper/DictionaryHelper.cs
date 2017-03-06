namespace System.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryHelper
    {
        public static IEnumerable<TValue> Match<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, bool> predicate)
            => dictionary.Where(x => predicate(x.Key)).Select(x => x.Value);

        public static IEnumerable<TValue> Match<T,TKey, TValue>(this IEnumerable<T> sequences,IDictionary<TKey, TValue> dictionary, Func<TKey, bool> predicate)
            => dictionary.Where(x => predicate(x.Key)).Select(x => x.Value);

        public static IEnumerable<TValue> Match<T, TKey, TValue>(this IEnumerable<T> sequences, Func<T, TKey> source, Func<T, TKey, bool> predicate, Func<TKey, TValue> result)
            => sequences.Select(x => (source: x, key: source(x)))
                        .Where(x => predicate(x.source, x.key))
                        .Select(x => result(x.key));

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key, Func<TValue> value)
            => map.TryAdd(key, value())[key];

        public static IDictionary<TKey, TValue> AddOrUpdate<TKey,TValue>(this IDictionary<TKey,TValue> map, TKey key,TValue value)
            => map.TryAdd(key, value);

        private static IDictionary<TKey, TValue> TryAdd<TKey,TValue>(this IDictionary<TKey, TValue> map,TKey key,TValue value = default(TValue))
        {
            if (!map.ContainsKey(key))
                map.Add(key, value);
            return map;
        }

        public static T AppendTo<T>(this T t,IEnumerable<T> list)
        {
            list.Append(t);
            return t;
        }
    }
}
