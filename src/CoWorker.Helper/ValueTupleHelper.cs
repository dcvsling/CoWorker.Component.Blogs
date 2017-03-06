using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace ExtremeGamerSIte
{
    public static class ValueTupleHelper
    {
        public static (T, TUse) Using<T, TUse>(this T t, TUse with)
            => (t, with);

        private static string GetValueTupleName(int length) => string.Format(typeof(ValueTuple).AssemblyQualifiedName.Replace("System.ValueTuple", "System.ValueTuple`{0}"), length);

        public static object ToTuple(this IEnumerable<Type> types)
            => Activator.CreateInstance(Type.GetType(GetValueTupleName(types.Count())).MakeGenericType(types.ToArray()));
    }
}
