namespace System.Linq
{
    using CoWorker.Helper.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class DefaultHelper
    {
        public static Maybe<T> DefaultMaybeIfNull<T>(this T t,T val = default(T))
            where T : class
            =>  Maybe<T>.New(!ReferenceEquals(t, null) ? t : val ?? default(T));
        
        public static T DefaultIfNull<T>(this T target, Func<T> getter) where T : class
            => target ?? getter();

        public static T DefaultIfNull<T>(this T target, T val) where T : class
            => target ?? val;

        public static Action<T> DefaultIfNull<T>(this Action<T> target, Action<T> val = null)
            => target ?? val ?? ActionHelper.Empty<T>();

        public static IEnumerable<T> EmptyIfNull<T>(this object t)
            where T : class
            => ReferenceEquals(t, null) 
                ? Enumerable.Empty<T>() 
                : t as IEnumerable<T> ?? Enumerable.Empty<T>();
        
        public static object Default(this Type type)
            => type.GetTypeInfo().IsValueType || type == typeof(string)
                ? Activator.CreateInstance(type) : null;

        public static IEnumerable<T> Default<T>(this Type type)
            => new[] { (T)type.Default() };

        public static IEnumerable<T> AsSingleEnumerable<T>(this T t) => new T[] { t };

        
    }
}
