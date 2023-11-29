using Microsoft.AspNetCore.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection;

public static class ExceptionHandlerServiceCollectionExtensions
{
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ArgumentExceptionHandler>();

        return services;
    }
}
