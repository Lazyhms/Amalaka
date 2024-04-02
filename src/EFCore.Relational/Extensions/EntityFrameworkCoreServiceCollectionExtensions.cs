using Amalaka.EntityFrameworkCore.Infrastructure.Internal;
using Amalaka.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Amalaka.EntityFrameworkCore.Extensions;

public static class EntityFrameworkCoreServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFrameworkCoreServices(this IServiceCollection serviceCollection)
    {
        new EntityFrameworkRelationalServicesBuilder(serviceCollection)
            .TryAdd<IConventionSetPlugin, ConventionSetPlugin>()
            .TryAdd<IMethodCallTranslatorPlugin, MethodCallTranslatorPlugin>()
            .TryAdd<IAggregateMethodCallTranslatorPlugin, AggregateMethodCallTranslatorPlugin>()
            .TryAdd<ISingletonOptions, INoneRelationalOptions>(p => p.GetRequiredService<INoneRelationalOptions>())
            .TryAddProviderSpecificServices(m =>
            {
                m.TryAddSingleton<INoneRelationalOptions, NoneRelationalOptions>();
            });
        return serviceCollection;
    }
}