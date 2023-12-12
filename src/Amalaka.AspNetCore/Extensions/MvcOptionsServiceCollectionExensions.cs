using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Microsoft.Extensions.DependencyInjection;

public static class MvcOptionsServiceCollectionExensions
{
    public static IServiceCollection ConfigureMvcOptions(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add<GlobalExceptionFilterAttribute>();
            options.Filters.Add<BusinessExceptionFilterAttribute>();
            options.Filters.Add<ArgumentExceptionFilterAttribute>();
            options.Filters.Add<ArgumentNullExceptionFilterAttribute>();
            options.Filters.Add<ObjectResultFilterAttribute>();
            options.ModelMetadataDetailsProviders.Add(new LocalizationModelValidationMetadataProvider());
        });
        return services;
    }
}