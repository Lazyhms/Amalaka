using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc.Filters;

public sealed class ArgumentExceptionFilterAttribute(ILogger<ArgumentExceptionFilterAttribute> logger, IWebHostEnvironment webHostEnvironment)
    : ExceptionFilterAttribute<ArgumentExceptionFilterAttribute, ArgumentException>(logger, webHostEnvironment)
{
    public override ProblemDetails ProblemDetails { get; set; } = new()
    {
        Title = "参数异常",
        Status = StatusCodes.Status202Accepted,
    };
}