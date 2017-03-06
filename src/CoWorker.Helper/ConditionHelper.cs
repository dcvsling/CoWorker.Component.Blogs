
namespace CoWorker.Helper
{
    using Microsoft.AspNetCore.Builder;
    using System;
    public static class ConditionHelper
    {
        public static T Case<T>(this T target, Func<bool> predicate, Action execute)
        {
            if (predicate()) execute();
            return target;
        }

        public static bool If(this IApplicationBuilder app, string key)
            => Convert.ToBoolean(app.Properties[key]);
    }
}
