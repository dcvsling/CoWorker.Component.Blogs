
namespace System.Reflection
{
    using System.Reflection;
    public static class TypeHelper
    {
        public static bool IsDelegate(this Type type)
            => typeof(Delegate).IsAssignableFrom(type);
    }
}
