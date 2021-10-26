﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NextStar.Framework.Abstractions.Auditing;
using NextStar.Framework.AspNetCore.Extensions;

namespace NextStar.Framework.AspNetCore.Auditing
{
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

        private bool ShouldSaveAudit(ActionExecutingContext context, out AuditLogActionInfo auditLogAction)
        {
            auditLogAction = null;

            if (!context.ActionDescriptor.IsControllerAction())
            {
                return false;
            }

            if (!ShouldSaveAudit(context.ActionDescriptor.GetMethodInfo()))
            {
                return false;
            }

            auditLogAction = CreateAuditLogAction(
                context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.AsType(),
                context.ActionDescriptor.AsControllerActionDescriptor().MethodInfo,
                context.ActionArguments
            );
            return true;
        }

        private AuditLogActionInfo CreateAuditLogAction(
            Type type,
            MethodInfo method,
            IDictionary<string, object> arguments)
        {
            var actionInfo = new AuditLogActionInfo
            {
                ServiceName = type != null
                    ? type.FullName
                    : "",
                MethodName = method.Name,
                Parameters = arguments,
                ExecutionTime = DateTime.UtcNow
            };

            return actionInfo;
        }
        public bool ShouldSaveAudit(MethodInfo methodInfo)
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
    }
}