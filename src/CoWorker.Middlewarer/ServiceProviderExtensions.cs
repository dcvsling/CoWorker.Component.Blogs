
namespace CoWorker.Middlewarer
{
    using CoWorker.Middlewarer.Abstractions;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// for IServiceProvider extensions method
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// get service has middleware we setting 
        /// </summary>
        /// <typeparam name="T">service type</typeparam>
        /// <param name="provider">service provider</param>
        /// <returns></returns>
        public static T GetServiceWithMiddleware<T>(this IServiceProvider provider)
            => provider.GetService<IMiddlewarerOptions<T>>().Value;
    }
}
