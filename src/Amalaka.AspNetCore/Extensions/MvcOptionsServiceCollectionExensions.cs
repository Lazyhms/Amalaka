using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Microsoft.Extensions.DependencyInjection;

public static class MvcOptionsServiceCollectionExensions
{
    public static IServiceCollection ConfigureMvcOptions(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(options =>
        {
            options.ModelMetadataDetailsProviders.Add(new LocalizationModelValidationMetadataProvider());
        });
        return services;
    }
}
