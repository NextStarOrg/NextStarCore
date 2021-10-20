using System;
using NextStar.Framework.AspNetCore.Enrichers;
using Serilog;
using Serilog.Configuration;

namespace NextStar.Framework.AspNetCore.Extensions
{
    public static class SessionLoggerExtensions
    {
        public static LoggerConfiguration WithNextStarSession(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null)
                throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<NextStarSessionEnricher>();
        }
    }
}