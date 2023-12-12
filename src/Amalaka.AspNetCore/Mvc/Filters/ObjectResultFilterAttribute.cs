namespace Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public sealed class ObjectResultFilterAttribute() : ResultFilterAttribute
{
    public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult okObjectResult)
        {
            context.Result = new OkObjectResult(new
            {
                Success = true,
                Message = "",
                Data = okObjectResult.Value
            });
        }
        return base.OnResultExecutionAsync(context, next);
    }
}