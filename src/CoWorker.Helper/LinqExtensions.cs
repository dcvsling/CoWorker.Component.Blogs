
namespace System.Linq
{
    using System.Collections.Generic;

    public static class LinqExtensions {

        public static IEnumerable<T> ExpectNull<T>(this IEnumerable<T> seq) => seq.Where(x => x != null);

        public static void Each<T>(this IEnumerable<T> seq, Action<T> action) => seq.ToList().ForEach(action);
    }
}
