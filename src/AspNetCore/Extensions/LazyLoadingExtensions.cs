using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection.Internal;

namespace Microsoft.Extensions.DependencyInjection;

public static class LazyLoadingExtensions
{
    public static IServiceCollection AddLazyLoadingAccessor(this IServiceCollection services)
    {
        services.TryAddTransient(typeof(ILazyLoading<>), typeof(LazyLoading<>));
        return services;
    }
}
