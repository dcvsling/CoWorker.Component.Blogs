
namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.Extensions.Options;
    using System.Linq;
    using System;
    using CoWorker.Helper.Options;

    public static class DIHelper
    {
        public static IServiceCollection AddSingleton<T>(this IServiceCollection services, IServiceProvider provider)
            where T : class
            => services.AddSingleton(provider?.GetRequiredService<T>());

        public static IServiceCollection AddTransient<T>(this IServiceCollection services, IServiceProvider provider)
            where T : class
            => services.AddTransient(x => provider?.GetRequiredService<T>());

        public static IServiceCollection AddScoped<T>(this IServiceCollection services, IServiceProvider provider)
            where T : class
            => services.AddScoped(x => provider?.GetRequiredService<T>());

        public static IServiceCollection TryAddSingleton(this IServiceCollection services, Type service, Type implementationType)
        {
            Extensions.ServiceCollectionDescriptorExtensions.TryAddSingleton(services,service,implementationType);
            return services;
        }

        public static IServiceCollection TryAddSingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class,TService
        {
            Extensions.ServiceCollectionDescriptorExtensions.TryAddSingleton<TService, TImplementation>(services);
            return services;
        }

        public static IServiceCollection TryAddSingleton<TService>(this IServiceCollection services,TService service)
            where TService : class
        {
            Extensions.ServiceCollectionDescriptorExtensions.TryAddSingleton(services,service);
            return services;
        }

        public static IServiceCollection Replace<TService>(this IServiceCollection services, ServiceDescriptor service)
            where TService : class
        {
            Extensions.ServiceCollectionDescriptorExtensions.Replace(services, service);
            return services;
        }
        public static IServiceCollection TryAddSingleton<TService>(
            this IServiceCollection services, 
            Func<IServiceProvider, TService> implementationFactory
        )   where TService : class
        {
            Extensions.ServiceCollectionDescriptorExtensions.Replace(services,new ServiceDescriptor(typeof(TService), implementationFactory,ServiceLifetime.Singleton));
            return services;
        }
        

        public static T GetServiceWithConfigure<T>(this IServiceProvider provider)
            where T :class
        {
            var service = provider.GetService<T>();
            (provider.GetService<IAggregateConfigureOptions<T>>() 
                ?? provider.GetService<IConfigureOptions<T>>()
                )?.Configure(service);
            return service;
        }



        public static IServiceCollection ConfigureWithConfiguration<T>(this IServiceCollection services)
            => services;
    }
}
