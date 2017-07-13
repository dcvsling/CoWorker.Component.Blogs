namespace CoWorker.Middlewarer
{
    using CoWorker.Middlewarer.Abstractions;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// IServiceCollection extensions method
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// register middleware class in to container
        /// </summary>
        /// <typeparam name="TService">service type you want to inject</typeparam>
        /// <typeparam name="TMiddleware">middleware class (or injector) type</typeparam>
        /// <param name="services">IServiceCollection</param>
        /// <param name="factory">factory of middleware class (or injector)</param>
        /// <returns></returns>
        public static IServiceCollection WithMiddleware<TService, TMiddleware>(
            this IServiceCollection services,
            Func<IServiceProvider, TMiddleware> factory = null
        )
            where TMiddleware : class,TService
            where TService : class
            => services
            .AddTransient<TMiddleware>()
            .AddTransient<IMiddlewarerOptions<TService>>(
                p => new MiddlewarerOptions<TService,TMiddleware>(p.GetService<TMiddleware>())
            );
                
    }
}
