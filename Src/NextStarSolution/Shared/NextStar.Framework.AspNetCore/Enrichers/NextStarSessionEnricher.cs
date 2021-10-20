using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NextStar.Framework.Core.Consts;
using Serilog.Core;
using Serilog.Events;

namespace NextStar.Framework.AspNetCore.Enrichers
{
    public class NextStarSessionEnricher:ILogEventEnricher
    {
        private const string IpAddressPropertyName = "ClientIp";
        private const string ClientAgentPropertyName = "ClientAgent";

        public ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public NextStarSessionEnricher() : this(new HttpContextAccessor())
        {
        }

        public NextStarSessionEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_httpContextAccessor.HttpContext == null) return;

            #region ClientIp

            var ipAddress = GetIpAddress();

            if (string.IsNullOrWhiteSpace(ipAddress))
                ipAddress = "unknown";

            var ipAddressProperty = new LogEventProperty(IpAddressPropertyName, new ScalarValue(ipAddress));

            logEvent.AddPropertyIfAbsent(ipAddressProperty);

            #endregion

            #region ClientAgent

            var agentName = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];

            logEvent.AddPropertyIfAbsent(new LogEventProperty(ClientAgentPropertyName, new ScalarValue(agentName)));

            #endregion

            #region Session

            if (SessionId == null) return;
            logEvent.AddPropertyIfAbsent(new LogEventProperty("SessionId", new ScalarValue(SessionId)));

            #endregion
        }

        #region IpAddress


        private string GetIpAddress()
        {
            var ipAddress = _httpContextAccessor.HttpContext.Request.Headers["X-forwarded-for"].FirstOrDefault();

            if (!string.IsNullOrEmpty(ipAddress))
            {
                return GetIpAddressFromProxy(ipAddress);
            }

            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        private string GetIpAddressFromProxy(string proxiedIpList)
        {
            var addresses = proxiedIpList.Split(',');

            if (addresses.Length != 0)
            {
                // If IP contains port, it will be after the last : (IPv6 uses : as delimiter and could have more of them)
                return addresses[0].Contains(":")
                    ? addresses[0].Substring(0, addresses[0].LastIndexOf(":", StringComparison.Ordinal))
                    : addresses[0];
            }

            return string.Empty;
        }

        #endregion

        #region Session

        private Guid? SessionId
        {
            get
            {
                var sessionIdClaim = Principal?.Claims.FirstOrDefault(c => c.Type == NextStarClaimTypes.SessionId);
                if (string.IsNullOrEmpty(sessionIdClaim?.Value))
                {
                    return null;
                }
                Guid sId;
                if (!Guid.TryParse(sessionIdClaim.Value, out sId))
                {
                    return null;
                }
                return sId;
            }
        }
        #endregion
    }
}