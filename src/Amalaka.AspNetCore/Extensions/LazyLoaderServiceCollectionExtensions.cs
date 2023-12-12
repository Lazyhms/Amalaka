using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection.Internal;

namespace Microsoft.Extensions.DependencyInjection;

public static class LazyLoaderServiceCollectionExtensions
{
    public static IServiceCollection AddLazyLoaderAccessor(this IServiceCollection services)
    {
        services.TryAddTransient(typeof(ILazyLoader<>), typeof(LazyLoader<>));
        return services;
    }
}
