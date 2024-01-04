namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityGeneratorServiceCollectionExtensions
{
    public static IServiceCollection AddSnowflakeIdentityGenerator(this IServiceCollection services, Action<SnowflakeOptions>? setupAction = null)
    {
        services.Configure(setupAction ??= (e) =>
        {
            e.MachingId = 0;
            e.DataCenterId = 0;
        });
        services.AddSingleton<ISnowflakeGenerator, SnowflakeGenerator>();
        return services;
    }
}