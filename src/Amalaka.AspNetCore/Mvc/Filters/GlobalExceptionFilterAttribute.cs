using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc.Filters;

public sealed class GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger, IWebHostEnvironment webHostEnvironment)
    : ExceptionFilterAttribute<GlobalExceptionFilterAttribute, Exception>(logger, webHostEnvironment)
{
    public override ProblemDetails ProblemDetails { get; set; } = new()
    {
        Title = "服务器异常",
        Detail = "服务器发生错误,请联系管理员",
        Status = StatusCodes.Status500InternalServerError,
    };
}