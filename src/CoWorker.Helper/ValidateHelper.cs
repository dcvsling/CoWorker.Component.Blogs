namespace CoWorker.Helper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerStepThrough]
    public static class ValidateHelper
    {
        [DebuggerHidden]
        public static T NotNull<T>(this T t,string name)
            => t.Throw<NullReferenceException>(name).ThrowIfTrue<T,NullReferenceException>(ReferenceEquals(t, null));

        [DebuggerHidden]
        public static IEnumerable<T> NotEmpty<T>(this IEnumerable<T> iterator,string message)
            => iterator.Throw<NullReferenceException>(message).ThrowIfTrue<IEnumerable<T>, NullReferenceException>(iterator.Any());

        [DebuggerHidden]
        private static T ThrowIfTrue<T,TException>(this TException ex, bool isThrow = false, Func<T, bool> predicate = null)
            where TException : Exception
            => (isThrow || (predicate?.Invoke(ex.Data.Get<T>("Value")) ?? false)) ? throw ex : ex.Data.Get<T>("Value");

        public static T Get<T>(this IDictionary map, object key)
            => map.Contains(key) ? (T)map[key] : default(T);
    }
}
