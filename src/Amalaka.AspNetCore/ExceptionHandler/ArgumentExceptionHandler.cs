using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Diagnostics;

public class ArgumentExceptionHandler(Func<HttpContext, bool>? action = null) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ArgumentException)
        {
            return false;
        }

        if (action != null)
        {
            return action!.Invoke(httpContext);
        }

        httpContext.RequestServices.GetRequiredService<ILogger<ArgumentExceptionHandler>>().LogError(exception, "Exception handled");

        httpContext.Response.StatusCode = 400;
        await httpContext.Response.WriteAsJsonAsync(new
        {
            exception.Message
        }, cancellationToken);

        return true;
    }
}