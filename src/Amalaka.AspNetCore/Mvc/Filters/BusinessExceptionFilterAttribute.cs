using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc.Filters;

public sealed class BusinessExceptionFilterAttribute(ILogger<BusinessExceptionFilterAttribute> logger, IWebHostEnvironment webHostEnvironment)
    : ExceptionFilterAttribute<BusinessExceptionFilterAttribute, BusinessException>(logger, webHostEnvironment)
{
    public override ProblemDetails ProblemDetails { get; set; } = new()
    {
        Title = "业务异常",
        Status = StatusCodes.Status202Accepted,
    };
}