namespace CoWorker.Helper
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    public static class GetterHelper
    {
        
        public static T Get<T>(this IApplicationBuilder app, string key)
            => app.Properties[key].CastTo<T>();

        public static string Get(this IApplicationBuilder app, string key)
            => app.Properties[key].ToString();

        public static T GetService<T>(this IApplicationBuilder app) => app.ApplicationServices.GetService<T>();

        public static T GetMax<T>(this T t) where T : struct => (T)Enum.ToObject(typeof(T),0-1);

        public static T GetMin<T>(this T t) where T : struct => (T)Enum.ToObject(typeof(T), 0);

        
    }
}
