namespace System.Collections.Generic
{
    using CoWorker.Helper.Models.Generic;
    using System;
    using System.Reflection;
    using System.Linq;
    using CoWorker.Helper.Models;

    public static class GenericListExtensions
    {
        public static IGenericList Add<T>(this IGenericList array, params object[] genericObj)
            => genericObj.Aggregate(
                array,
                (ary, obj) =>
                {
                    ary.Add(obj.GetType(), obj); return ary;
                });

        public static T Find<T>(this IGenericList array)
        {
            var equal = array.Where(x => typeof(T).GetTypeInfo().IsAssignableFrom(x.Key));
            return equal.FirstOrDefault(x => x.Key == typeof(T)).Value.Cast<T>();
        }

        private static IEnumerable<T> GetAssignable<T>(this IGenericList array,Type type)
            => array.Where(x => typeof(T).GetTypeInfo().IsAssignableFrom(x.Key)).SelectMany(x => x.Value.Cast<T>());

        public static IGenericList Get(this IGenericList array, params Type[] types)
            => new GenericList(array.Where(x => types.Contains(x.Key)));

        public static IGenericList CreateList<T>(this Maybe<T> obj, bool isSync = true)
            => new GenericList(isSync)
            {
                { typeof(T), obj }
            };
    }
}
