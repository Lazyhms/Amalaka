using System.IdentityGenerator;
using System.IdentityGenerator.Internal;

namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityGeneratorServiceCollectionExtensions
{
    public static IServiceCollection AddSnowflakeIdentityGenerator(this IServiceCollection services, Action<SnowflakeOptions>? setupAction = null)
    {
        services.Configure(setupAction ??= (e) =>
        {
            e.DataCenterId = 0;
            e.MachingId = 0;
        });
        services.AddSingleton<IIdentityGenerator, SnowflakeGenerator>();
        return services;
    }
}
