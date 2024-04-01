using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Microsoft.Extensions.DependencyInjection;

public static class MvcServiceExensions
{
    public static IServiceCollection ConfigureMvcOptions(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add<ObjectResultFilterAttribute>();
            options.Filters.Add<GlobalExceptionFilterAttribute>();
            options.Filters.Add<BusinessExceptionFilterAttribute>();
            options.ModelMetadataDetailsProviders.Add(new LocalizationModelValidationMetadataProvider());
        });
        return services;
    }
}