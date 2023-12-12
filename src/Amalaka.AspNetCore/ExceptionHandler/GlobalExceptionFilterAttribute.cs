using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Diagnostics;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment webHostEnvironment)
    : ExceptionHandler<GlobalExceptionHandler, Exception>(logger, webHostEnvironment)
{
    public override ProblemDetails ProblemDetails { get; set; } = new()
    {
        Title = "服务器异常",
        Detail = "服务器发生错误,请联系管理员",
        Status = StatusCodes.Status500InternalServerError,
    };
}