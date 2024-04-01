using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Diagnostics;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment webHostEnvironment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is Exception handledException)
        {
            logger.LogError(exception, "Title:服务器异常 HResult:{HResult}", handledException.HResult);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                Code = 2,
                Message = webHostEnvironment.IsDevelopment()
                            ? handledException.ToString()
                            : "服务器发生错误,请联系管理员"
            }, cancellationToken);

            return true;
        }
        return false;
    }
}