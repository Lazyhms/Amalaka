using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class RegisterAssemblyServiceCollectionExtensions
{
    private static readonly IReadOnlyCollection<Type> _dependencyTypeMapping = new[] { typeof(IScoped), typeof(ISingleton), typeof(ITransient) };

    public static IServiceCollection RegisterDependencyServiceFromAssembly(this IServiceCollection services, string assemblyName)
    {
        return services.RegisterDependencyServiceFromAssembly(Assembly.Load(assemblyName));
    }

    public static IServiceCollection RegisterDependencyServiceFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var assemblyServiceTypes = assembly.GetTypes();

        assemblyServiceTypes.Where(w => w.IsInterface && (w.IsDefined(typeof(DependencyAttribute)) || _dependencyTypeMapping.Any(a => a.IsAssignableFrom(w)))).ForEach(serviceType =>
        {
            var serviceDescriptor = serviceType.GetCustomAttribute<DependencyAttribute>();
            var implementationTypes = assemblyServiceTypes.Where(implementationType => serviceType.IsAssignableFrom(implementationType) && serviceType != implementationType);
            if (implementationTypes is not null && implementationTypes.Any())
            {
                implementationTypes.ForEach(implementationType =>
                {
                    if (typeof(IScoped).IsAssignableFrom(serviceType) || ServiceLifetime.Scoped == serviceDescriptor?.ServiceLifetime)
                    {
                        services.TryAddScoped(serviceType, implementationType!);
                    }
                    if (typeof(ISingleton).IsAssignableFrom(serviceType) || ServiceLifetime.Singleton == serviceDescriptor?.ServiceLifetime)
                    {
                        services.TryAddSingleton(serviceType, implementationType!);
                    }
                    if (typeof(ITransient).IsAssignableFrom(serviceType) || ServiceLifetime.Transient == serviceDescriptor?.ServiceLifetime)
                    {
                        services.TryAddTransient(serviceType, implementationType!);
                    }
                });
            }
        });

        return services;
    }
}