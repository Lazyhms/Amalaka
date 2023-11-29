using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;
public static class ApiBehaviorOptionsServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApiBehaviorInvalidModelStateResponse(this IServiceCollection services, Func<ActionContext, IActionResult>? invalidModelStateResponse = null)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                if (invalidModelStateResponse != null)
                {
                    return invalidModelStateResponse!.Invoke(context);
                }

                return new BadRequestObjectResult(context.ModelState);
            };
        });
        return services;
    }
}
