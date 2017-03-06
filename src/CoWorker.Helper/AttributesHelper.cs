namespace System
{
    using System.Collections.Generic;
    using System.Reflection;

    public static class AttributesHelper
    {
        public static TAttr GetAttr<TAttr>(this object obj, bool inherit = false)
            where TAttr : Attribute
            => obj.GetType().GetTypeInfo().GetCustomAttribute<TAttr>(inherit);

        public static IEnumerable<TAttr> GetAttrs<TAttr>(this object obj, bool inherit = false)
            where TAttr : Attribute
            => obj.GetType().GetTypeInfo().GetCustomAttributes<TAttr>(inherit);

        public static IEnumerable<CustomAttributeData> GetAttrsData(this object obj)
            => obj.GetType().GetTypeInfo().CustomAttributes;

        public static TResult UseAttr<T,TAttr,TResult>(this T t,Func<T,TAttr,TResult> func) where TAttr : Attribute
            => func(t, t.GetAttr<TAttr>());
    }
}
