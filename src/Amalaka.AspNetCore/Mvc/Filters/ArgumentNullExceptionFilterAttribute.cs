using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc.Filters;

public sealed class ArgumentNullExceptionFilterAttribute(ILogger<ArgumentNullExceptionFilterAttribute> logger, IWebHostEnvironment webHostEnvironment)
    : ExceptionFilterAttribute<ArgumentNullExceptionFilterAttribute, ArgumentNullException>(logger, webHostEnvironment)
{
    public override ProblemDetails ProblemDetails { get; set; } = new()
    {
        Title = "参数空异常",
        Status = StatusCodes.Status202Accepted,
    };
}