using NextStar.Framework.EntityFrameworkCore.Input.Consts;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System.IO;
using NextStar.Framework.Abstractions.AppSetting;

namespace NextStar.Framework.AspNetCore.Extensions
{
    public static class LoggerExtension
    {
        /// <summary>
        /// 添加日志到文件中
        /// </summary>
        /// <param name="writeTo"></param>
        /// <param name="appSetting"></param>
        /// <returns></returns>
        public static LoggerConfiguration AddNextStarLoggers(this LoggerSinkConfiguration writeTo,
            AppSetting appSetting)
        {
            var serverName = appSetting.SerilogSetting.ServerName;
            var errorConfiguration = appSetting.SerilogSetting.ErrorConfig;
            var normalConfiguration = appSetting.SerilogSetting.NormalConfig;
            var businessConfiguration = appSetting.SerilogSetting.BusinessConfig;

            // 错误日志
            return writeTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Level == LogEventLevel.Error ||
                            le.Level == LogEventLevel.Fatal)
                        .WriteTo.File(new CompactJsonFormatter(), Path.Combine(errorConfiguration.Path, serverName + "_error_.log"), restrictedToMinimumLevel: LogEventLevel.Warning, fileSizeLimitBytes: errorConfiguration.FileSizeLimitBytes, retainedFileCountLimit: errorConfiguration.RetainedFileCountLimit, rollingInterval: RollingInterval.Day);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Level == LogEventLevel.Warning)
                        .WriteTo.File(new CompactJsonFormatter(), Path.Combine(errorConfiguration.Path, serverName + "_warning_.log"), restrictedToMinimumLevel: LogEventLevel.Warning, fileSizeLimitBytes: errorConfiguration.FileSizeLimitBytes, retainedFileCountLimit: errorConfiguration.RetainedFileCountLimit, rollingInterval: RollingInterval.Day);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Properties.TryGetValue("SourceContext", out var pValue) && pValue != null &&
                            pValue.ToString().Contains("NextStarAuditActionFilter"))
                        .WriteTo.File(new CompactJsonFormatter(), Path.Combine(normalConfiguration.Path, serverName + "_audit_.log"), restrictedToMinimumLevel: LogEventLevel.Information, fileSizeLimitBytes: normalConfiguration.FileSizeLimitBytes, retainedFileCountLimit: normalConfiguration.RetainedFileCountLimit, rollingInterval: RollingInterval.Day);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Properties.TryGetValue("SourceContext", out var pValue) && pValue != null &&
                            pValue.ToString().Contains("BusinessLogger"))
                        .WriteTo.File(new CompactJsonFormatter(), Path.Combine(normalConfiguration.Path, serverName + "_business_.log"), restrictedToMinimumLevel: LogEventLevel.Information, fileSizeLimitBytes: normalConfiguration.FileSizeLimitBytes, retainedFileCountLimit: normalConfiguration.RetainedFileCountLimit, rollingInterval: RollingInterval.Day);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Level == LogEventLevel.Information
                            && !(le.Properties.TryGetValue("SourceContext", out var pValue) && pValue != null &&
                                 pValue.ToString().Contains("NextStarAuditActionFilter"))
                            && !(le.Properties.TryGetValue("SourceContext", out var pValue1) && pValue1 != null &&
                                 pValue1.ToString().Contains("BusinessLogger")))
                        .WriteTo.File(new CompactJsonFormatter(), Path.Combine(normalConfiguration.Path, serverName + "_normal_.log"), restrictedToMinimumLevel: LogEventLevel.Information, fileSizeLimitBytes: normalConfiguration.FileSizeLimitBytes, retainedFileCountLimit: normalConfiguration.RetainedFileCountLimit, rollingInterval: RollingInterval.Day);
                });
        }

        /// <summary>
        /// 添加日志到文件中
        /// </summary>
        /// <param name="writeTo"></param>
        /// <param name="appSetting"></param>
        /// <returns></returns>
        public static LoggerConfiguration AddNextStarLoggersWithoutAudit(this LoggerSinkConfiguration writeTo,
            AppSetting appSetting)
        {
            var serverName = appSetting.SerilogSetting.ServerName;
            var errorConfiguration = appSetting.SerilogSetting.ErrorConfig;
            var normalConfiguration = appSetting.SerilogSetting.NormalConfig;
            var businessConfiguration = appSetting.SerilogSetting.BusinessConfig;

            // 错误日志
            return writeTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Level == LogEventLevel.Error ||
                            le.Level == LogEventLevel.Fatal)
                        .WriteTo.File(new CompactJsonFormatter(), Path.Combine(errorConfiguration.Path, serverName + "_error_.log"), restrictedToMinimumLevel: LogEventLevel.Warning, fileSizeLimitBytes: errorConfiguration.FileSizeLimitBytes, retainedFileCountLimit: errorConfiguration.RetainedFileCountLimit, rollingInterval: RollingInterval.Day);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Level == LogEventLevel.Warning)
                        .WriteTo.File(new CompactJsonFormatter(), Path.Combine(errorConfiguration.Path, serverName + "_warning_.log"), restrictedToMinimumLevel: LogEventLevel.Warning, fileSizeLimitBytes: errorConfiguration.FileSizeLimitBytes, retainedFileCountLimit: errorConfiguration.RetainedFileCountLimit, rollingInterval: RollingInterval.Day);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Properties.TryGetValue("SourceContext", out var pValue) && pValue != null &&
                            pValue.ToString().Contains("BusinessLogger"))
                        .WriteTo.File(new CompactJsonFormatter(), Path.Combine(normalConfiguration.Path, serverName + "_business_.log"), restrictedToMinimumLevel: LogEventLevel.Information, fileSizeLimitBytes: normalConfiguration.FileSizeLimitBytes, retainedFileCountLimit: normalConfiguration.RetainedFileCountLimit, rollingInterval: RollingInterval.Day);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Level == LogEventLevel.Information
                            && !(le.Properties.TryGetValue("SourceContext", out var pValue1) && pValue1 != null &&
                                 pValue1.ToString().Contains("BusinessLogger")))
                        .WriteTo.File(new CompactJsonFormatter(), Path.Combine(normalConfiguration.Path, serverName + "_normal_.log"), restrictedToMinimumLevel: LogEventLevel.Information, fileSizeLimitBytes: normalConfiguration.FileSizeLimitBytes, retainedFileCountLimit: normalConfiguration.RetainedFileCountLimit, rollingInterval: RollingInterval.Day);
                });
        }
    }
}