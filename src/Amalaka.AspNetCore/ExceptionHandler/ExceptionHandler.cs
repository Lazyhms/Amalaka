using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Diagnostics;

public abstract class ExceptionHandler<TExceptionHandler, TException>(ILogger<TExceptionHandler> logger, IWebHostEnvironment webHostEnvironment) : IExceptionHandler
        where TExceptionHandler : ExceptionHandler<TExceptionHandler, TException>
        where TException : Exception
{
    public abstract ProblemDetails ProblemDetails { get; set; }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is TException handledException)
        {
            logger.LogError(exception: exception, "Title:{Title} HResult:{HResult}", ProblemDetails.Title, handledException.HResult);

            httpContext.Response.StatusCode = ProblemDetails.Status ?? StatusCodes.Status500InternalServerError;
            if (webHostEnvironment.IsDevelopment())
            {
                ProblemDetails.Detail = handledException.Message;
            }
            await httpContext.Response.WriteAsJsonAsync(ProblemDetails, cancellationToken);

            return true;
        }

        return false;
    }
}