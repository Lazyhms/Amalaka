using Amalaka.EntityFrameworkCore.Infrastructure.Internal;
using Amalaka.EntityFrameworkCore.Internal;
using Amalaka.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace Amalaka.EntityFrameworkCore.Extensions;

public static class EntityFrameworkCoreServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFrameworkCoreServices(this IServiceCollection serviceCollection)
    {
        new EntityFrameworkRelationalServicesBuilder(serviceCollection)
            .TryAdd<IConventionSetPlugin, NoneRelationalConventionSetPlugin>()
            .TryAdd<ISingletonOptions, INoneRelationalOptions>(p => p.GetRequiredService<INoneRelationalOptions>())
            .TryAddProviderSpecificServices(m =>
            {
                m.TryAddSingleton<INoneRelationalOptions, NoneRelationalOptions>();
            });
        return serviceCollection;
    }
}