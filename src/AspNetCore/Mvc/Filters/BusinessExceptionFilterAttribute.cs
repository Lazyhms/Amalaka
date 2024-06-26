﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc.Filters;

public sealed class BusinessExceptionFilterAttribute(ILogger<BusinessExceptionFilterAttribute> logger) : ExceptionFilterAttribute
{
    public override async Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is BusinessException handledException)
        {
            logger.LogError(handledException, "Title:业务异常 HResult:{HResult}", handledException.HResult);

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            await context.HttpContext.Response.WriteAsJsonAsync(new
            {
                Code = 1,
                handledException.Message,
            });
        }
        await base.OnExceptionAsync(context);
    }
}