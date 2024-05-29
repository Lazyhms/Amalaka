using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class RegisterAssemblyServiceCollectionExtensions
{
    private static readonly IReadOnlyCollection<Type> _dependencyTypeMapping = [typeof(IScoped), typeof(ISingleton), typeof(ITransient)];

    public static IServiceCollection RegisterDependencyServiceFromAssembly(this IServiceCollection services, string assemblyName)
    {
        return services.RegisterDependencyServiceFromAssembly(Assembly.Load(assemblyName));
    }

    public static IServiceCollection RegisterDependencyServiceFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        assembly.GetTypes().Where(w => w.IsInterface && _dependencyTypeMapping.Any(a => a.IsAssignableFrom(w))).ForEach(serviceType =>
        {
            var implementationTypes = assembly.GetTypes().Where(w => w.IsClass && serviceType.IsAssignableFrom(w));
            if (implementationTypes is not null && implementationTypes.Any())
            {
                foreach (var implementationType in implementationTypes)
                {
                    var serviceDescriptor = serviceType.GetCustomAttribute<DependencyAttribute>();
                    if (typeof(IScoped).IsAssignableFrom(serviceType))
                    {
                        if (serviceDescriptor?.StoredKey == null)
                        {
                            services.AddScoped(serviceType, implementationType!);
                        }
                        else
                        {
                            services.AddKeyedScoped(serviceType, serviceDescriptor.StoredKey, implementationType!);
                        }
                    }
                    if (typeof(ISingleton).IsAssignableFrom(serviceType))
                    {
                        if (serviceDescriptor?.StoredKey == null)
                        {
                            services.AddSingleton(serviceType, implementationType!);
                        }
                        else
                        {
                            services.AddKeyedSingleton(serviceType, serviceDescriptor.StoredKey, implementationType!);
                        }
                    }
                    if (typeof(ITransient).IsAssignableFrom(serviceType))
                    {
                        if (serviceDescriptor?.StoredKey == null)
                        {
                            services.AddTransient(serviceType, implementationType!);
                        }
                        else
                        {
                            services.AddKeyedTransient(serviceType, serviceDescriptor.StoredKey, implementationType!);
                        }
                    }
                }
            }
        });

        return services;
    }
}