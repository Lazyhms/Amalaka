using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class RegisterAssemblyServiceCollectionExtensions
{
    public static IServiceCollection RegisterAssembly(this IServiceCollection services, string assemblyName)
    {
        return services.RegisterAssembly(Assembly.Load(assemblyName));
    }

    public static IServiceCollection RegisterAssembly(this IServiceCollection services, Assembly assembly)
    {
        var assemblyServiceTypes = assembly.GetTypes();

        assemblyServiceTypes.Where(serviceType => serviceType.IsInterface && serviceType.IsDefined(typeof(DependencyAttribute))).ForEach(serviceType =>
        {
            var serviceDescriptor = serviceType.GetCustomAttribute<DependencyAttribute>();
            var implementationTypes = assemblyServiceTypes.Where(implementationType => serviceType.IsAssignableFrom(implementationType) && serviceType != implementationType);
            if (implementationTypes is not null && implementationTypes.Any())
            {
                foreach (var implementationType in implementationTypes)
                {
                    switch (serviceDescriptor!.ServiceLifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.TryAddScoped(serviceType, implementationType!);
                            break;
                        case ServiceLifetime.Singleton:
                            services.TryAddSingleton(serviceType, implementationType!);
                            break;
                        case ServiceLifetime.Transient:
                            services.TryAddTransient(serviceType, implementationType!);
                            break;
                    }
                }
            }
        });

        assemblyServiceTypes.Where(w => w.IsInterface && new[] { typeof(IScoped), typeof(ISingleton), typeof(ITransient) }.Any(a => a.IsAssignableFrom(w))).ForEach(serviceType =>
        {
            var implementationTypes = assemblyServiceTypes.Where(implementationType => serviceType.IsAssignableFrom(implementationType) && serviceType != implementationType);
            if (implementationTypes is not null && implementationTypes.Any())
            {
                implementationTypes.ForEach(implementationType =>
                {
                    if (typeof(IScoped).IsAssignableFrom(serviceType))
                    {
                        services.TryAddScoped(serviceType, implementationType!);
                    }
                    if (typeof(ISingleton).IsAssignableFrom(serviceType))
                    {
                        services.TryAddSingleton(serviceType, implementationType!);
                    }
                    if (typeof(ITransient).IsAssignableFrom(serviceType))
                    {
                        services.TryAddTransient(serviceType, implementationType!);
                    }
                });
            }
        });

        return services;
    }
}