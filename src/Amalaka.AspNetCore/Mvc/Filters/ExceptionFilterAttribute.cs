using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc.Filters;

public abstract class ExceptionFilterAttribute<TExceptionHandler, TException>(ILogger<TExceptionHandler> logger, IWebHostEnvironment webHostEnvironment) : ExceptionFilterAttribute
        where TExceptionHandler : ExceptionFilterAttribute<TExceptionHandler, TException>
        where TException : Exception
{
    public abstract ProblemDetails ProblemDetails { get; set; }

    public override Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is TException handledException)
        {
            logger.LogError(exception: context.Exception, "Title:{Title} HResult:{HResult}", ProblemDetails.Title, handledException.HResult);

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = ProblemDetails.Status ?? StatusCodes.Status500InternalServerError;

            if (webHostEnvironment.IsDevelopment())
            {
                ProblemDetails.Detail = handledException.Message;
            }
            context.HttpContext.Response.WriteAsJsonAsync(ProblemDetails);
        }
        return base.OnExceptionAsync(context);
    }
}