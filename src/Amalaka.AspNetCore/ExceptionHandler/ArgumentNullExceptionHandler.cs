using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Diagnostics;

public sealed class ArgumentNullExceptionHandler(ILogger<ArgumentNullExceptionHandler> logger, IWebHostEnvironment webHostEnvironment)
    : ExceptionHandler<ArgumentNullExceptionHandler, ArgumentNullException>(logger, webHostEnvironment)
{
    public override ProblemDetails ProblemDetails { get; set; } = new()
    {
        Title = "参数空异常",
        Status = StatusCodes.Status202Accepted,
    };
}