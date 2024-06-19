using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Microsoft.Extensions.DependencyInjection;

public static class MvcJsonServiceExtensions
{
    public static IServiceCollection ConfigureMvcJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(configureOptions => configureOptions.JsonSerializerOptions.ApplyDefault());
        return services;
    }
}
