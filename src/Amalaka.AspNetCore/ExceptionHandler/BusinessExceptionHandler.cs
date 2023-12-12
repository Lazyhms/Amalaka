using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Diagnostics;

public sealed class BusinessExceptionHandler(ILogger<BusinessExceptionHandler> logger, IWebHostEnvironment webHostEnvironment)
    : ExceptionHandler<BusinessExceptionHandler, BusinessException>(logger, webHostEnvironment)
{
    public override ProblemDetails ProblemDetails { get; set; } = new()
    {
        Title = "业务异常",
        Status = StatusCodes.Status202Accepted,
    };
}