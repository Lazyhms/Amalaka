﻿using System.Text.Json;

namespace Microsoft.Extensions.DependencyInjection;

public static class HttpJsonServiceExtensions
{
    public static IServiceCollection ConfigureHttpJsonOptions(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(configureOptions => configureOptions.SerializerOptions.ApplyDefault());
        return services;
    }
}
