using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public sealed class ObjectResultFilterAttribute() : ResultFilterAttribute
{
    public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult
            && (objectResult.StatusCode is null || objectResult.StatusCode == StatusCodes.Status200OK))
        {
            context.Result = new OkObjectResult(new
            {
                Code = 0,
                Message = "操作成功",
                Data = objectResult.Value
            });
        }
        return base.OnResultExecutionAsync(context, next);
    }
}