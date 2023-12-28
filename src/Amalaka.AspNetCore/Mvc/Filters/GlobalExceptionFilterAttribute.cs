using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc.Filters;

public sealed class GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger, IWebHostEnvironment webHostEnvironment) : ExceptionFilterAttribute
{
    public override async Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is Exception handledException)
        {
            logger.LogError(context.Exception, "Title:服务器异常 HResult:{HResult}", handledException.HResult);

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.HttpContext.Response.WriteAsJsonAsync(new
            {
                Code = 2,
                Message = webHostEnvironment.IsDevelopment() 
                            ? handledException.Message 
                            : "服务器发生错误,请联系管理员"
            });
        }
        await base.OnExceptionAsync(context);
    }
}