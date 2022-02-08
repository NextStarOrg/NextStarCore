using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NextStar.Library.MicroService.Exceptions;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.Library.MicroService.Filters;

public class ServiceApplicationExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order { get; } = int.MaxValue - 10;
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is ServiceApplicationException exception)
        {
            context.Result = new JsonResult(new CommonDto<object>("500",exception.Message));
            context.ExceptionHandled = true;
        }
        else
        {
            context.Result = new JsonResult(new CommonDto<object>("500","发生未知错误信息，请求失败。"));
            context.ExceptionHandled = true;
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {

    }
}