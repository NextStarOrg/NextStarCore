using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.AspNetCore.Extensions;

namespace NextStar.Library.AspNetCore.Audit;

/// <summary>
/// 审计日志
/// </summary>
public class NextStarAuditActionFilter : IAsyncActionFilter
{
    private readonly ILogger<NextStarAuditActionFilter> _logger;

    public NextStarAuditActionFilter(ILogger<NextStarAuditActionFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!ShouldSaveAudit(context, out var auditLogAction))
        {
            await next();
            return;
        }

        if (auditLogAction == null)
        {
            _logger.LogInformation("AUDIT LOG: Audit Log Action Is Null");
            await next();
            return;
        }

        var stopwatch = Stopwatch.StartNew();
        try
        {
            var result = await next();
            if (result.Exception != null && !result.ExceptionHandled)
            {
                auditLogAction.Exception = result.Exception;
            }
        }
        catch (Exception ex)
        {
            auditLogAction.Exception = ex;
            throw;
        }
        finally
        {
            stopwatch.Stop();
            auditLogAction.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
            _logger.LogInformation("AUDIT LOG: {@AuditAction}", auditLogAction);
        }
    }

    private bool ShouldSaveAudit(ActionExecutingContext context, out AuditLogActionInfo? auditLogAction)
    {
        auditLogAction = null;

        var methodInfo = context.ActionDescriptor.GetMethodInfo();

        if (!context.ActionDescriptor.IsControllerAction())
        {
            return false;
        }

        if (!ShouldSaveAudit(methodInfo))
        {
            return false;
        }

        if (!ShouldSaveParameter(methodInfo))
        {
            auditLogAction = CreateAuditLogAction(
                context.ActionDescriptor.AsControllerActionDescriptor()?.ControllerTypeInfo.AsType(),
                context.ActionDescriptor.AsControllerActionDescriptor()?.MethodInfo,
                new Dictionary<string, object?>() { { "Ignore Data Auditing", "null" } }
            );
            return true;
        }

        auditLogAction = CreateAuditLogAction(
            context.ActionDescriptor.AsControllerActionDescriptor()?.ControllerTypeInfo.AsType(),
            context.ActionDescriptor.AsControllerActionDescriptor()?.MethodInfo,
            context.ActionArguments
        );
        return true;
    }

    private AuditLogActionInfo CreateAuditLogAction(
        Type? type,
        MethodInfo? method,
        IDictionary<string, object?>? arguments = null)
    {
        var actionInfo = new AuditLogActionInfo
        {
            ServiceName = type?.Name ?? "NullServiceName",
            MethodName = method?.Name ?? "NullMethodName",
            Parameters = arguments ?? new Dictionary<string, object?>(),
            ExecutionTime = DateTime.UtcNow
        };

        return actionInfo;
    }

    private bool ShouldSaveAudit(MethodInfo? methodInfo)
    {
        if (methodInfo == null)
        {
            return false;
        }

        if (!methodInfo.IsPublic)
        {
            return false;
        }

        if (methodInfo.IsDefined(typeof(DisableAuditingAttribute), true))
        {
            return false;
        }

        return true;
    }

    private bool ShouldSaveParameter(MethodInfo? methodInfo)
    {
        if (methodInfo == null)
        {
            return true;
        }

        if (!methodInfo.IsPublic)
        {
            return true;
        }

        if (methodInfo.IsDefined(typeof(IgnoreDataAuditingAttribute), true))
        {
            return false;
        }

        return true;
    }
}