using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class MvcJsonServiceExtensions
{
    public static IServiceCollection ConfigureMvcJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(configureOptions => configureOptions.JsonSerializerOptions.Default());
        return services;
    }
}
