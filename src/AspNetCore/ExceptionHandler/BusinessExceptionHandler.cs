using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Diagnostics;

public sealed class BusinessExceptionHandler(ILogger<BusinessExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is Exception handledException)
        {
            logger.LogError(exception, "Title:业务异常 HResult:{HResult}", handledException.HResult);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                Code = 2,
                handledException.Message
            }, cancellationToken);

            return true;
        }
        return false;
    }
}