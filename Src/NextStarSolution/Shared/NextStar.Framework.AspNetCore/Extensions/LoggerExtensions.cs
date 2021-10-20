using System.IO;
using Microsoft.Extensions.Configuration;
using NextStar.Framework.Core.Consts;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;

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
                        .WriteTo.RollingFile(new CompactJsonFormatter(),
                            Path.Combine(errorConfiguration.Path, serverName + "-error.json"), LogEventLevel.Warning,
                            errorConfiguration.FileSizeLimitBytes, errorConfiguration.RetainedFileCountLimit);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Level == LogEventLevel.Warning)
                        .WriteTo.RollingFile(new CompactJsonFormatter(),
                            Path.Combine(errorConfiguration.Path, serverName + "-warning.json"), LogEventLevel.Warning,
                            errorConfiguration.FileSizeLimitBytes, errorConfiguration.RetainedFileCountLimit);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Properties.TryGetValue("SourceContext", out var pValue) && pValue != null &&
                            pValue.ToString().Contains("NextStarAuditActionFilter"))
                        .WriteTo.RollingFile(new CompactJsonFormatter(),
                            Path.Combine(normalConfiguration.Path, serverName + "-audit.json"),
                            LogEventLevel.Information, normalConfiguration.FileSizeLimitBytes,
                            normalConfiguration.RetainedFileCountLimit);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Properties.TryGetValue("SourceContext", out var pValue) && pValue != null &&
                            pValue.ToString().Contains("BusinessLogger"))
                        .WriteTo.RollingFile(new CompactJsonFormatter(),
                            Path.Combine(businessConfiguration.Path, serverName + "-business.json"),
                            LogEventLevel.Information, businessConfiguration.FileSizeLimitBytes,
                            businessConfiguration.RetainedFileCountLimit);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Level == LogEventLevel.Information
                            && !(le.Properties.TryGetValue("SourceContext", out var pValue) && pValue != null &&
                                 pValue.ToString().Contains("NextStarAuditActionFilter"))
                            && !(le.Properties.TryGetValue("SourceContext", out var pValue1) && pValue1 != null &&
                                 pValue1.ToString().Contains("BusinessLogger")))
                        .WriteTo.RollingFile(new CompactJsonFormatter(),
                            Path.Combine(normalConfiguration.Path, serverName + "-normal.json"),
                            LogEventLevel.Information, normalConfiguration.FileSizeLimitBytes,
                            normalConfiguration.RetainedFileCountLimit);
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
                        .WriteTo.RollingFile(new CompactJsonFormatter(),
                            Path.Combine(errorConfiguration.Path, serverName + "-error.json"), LogEventLevel.Warning,
                            errorConfiguration.FileSizeLimitBytes, errorConfiguration.RetainedFileCountLimit);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Level == LogEventLevel.Warning)
                        .WriteTo.RollingFile(new CompactJsonFormatter(),
                            Path.Combine(errorConfiguration.Path, serverName + "-warning.json"), LogEventLevel.Warning,
                            errorConfiguration.FileSizeLimitBytes, errorConfiguration.RetainedFileCountLimit);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Properties.TryGetValue("SourceContext", out var pValue) && pValue != null &&
                            pValue.ToString().Contains("BusinessLogger"))
                        .WriteTo.RollingFile(new CompactJsonFormatter(),
                            Path.Combine(businessConfiguration.Path, serverName + "-business.json"),
                            LogEventLevel.Information, businessConfiguration.FileSizeLimitBytes,
                            businessConfiguration.RetainedFileCountLimit);
                })
                .WriteTo.Logger(loggerConfig =>
                {
                    loggerConfig.Filter.ByIncludingOnly(le =>
                            le.Level == LogEventLevel.Information
                            && !(le.Properties.TryGetValue("SourceContext", out var pValue1) && pValue1 != null &&
                                 pValue1.ToString().Contains("BusinessLogger")))
                        .WriteTo.RollingFile(new CompactJsonFormatter(),
                            Path.Combine(normalConfiguration.Path, serverName + "-normal.json"),
                            LogEventLevel.Information, normalConfiguration.FileSizeLimitBytes,
                            normalConfiguration.RetainedFileCountLimit);
                });
        }
    }
}