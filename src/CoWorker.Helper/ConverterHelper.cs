namespace System
{
    using System.Linq;

    public static class ConverterHelper
    {
        public static Lazy<T> ToLazyWithFactory<T>(this Func<T> factory)
            => new Lazy<T>(factory);

        public static Lazy<T> ToLazy<T>(this Type type, params Func<object>[] argsGetter)
            where T : class
            => new Lazy<T>(() => Activator.CreateInstance(type, argsGetter.Select(x => x()).ToArray()) as T);

        public static TResult CastTo<TSource, TResult>(this TSource source, Func<TSource, TResult> selector = null)
            where TResult : class
            => (selector ?? (x => x as TResult))(source);

        public static T CastTo<T>(this object source)
            => source is T ? (T)source : default(T);
    }
}
