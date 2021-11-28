using System.Security.Claims;
using IdentityModel;
using NextStar.Library.Core.Consts;

namespace NextStar.Library.AspNetCore.Extensions;

public static class ClaimsPrincipalExtensions
{
        /// <summary>
        /// 获取Cookie或者access token中SessionId
        /// </summary>
        /// <param name="Principal"></param>
        /// <returns></returns>
        public static Guid? GetNextStarSessionId(this ClaimsPrincipal Principal)
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
        
        public static Guid? GetNextStarUserKey(this ClaimsPrincipal principal)
        {
            var claim = principal?.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject);
            if (string.IsNullOrEmpty(claim?.Value))
            {
                return null;
            }
            Guid sId;
            if (!Guid.TryParse(claim.Value, out sId))
            {
                return null;
            }
            return sId;
        }

        public static string GetNextStarName(this ClaimsPrincipal principal)
        {
            var claim = principal?.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Name);
            return claim == null ? string.Empty : claim.Value;
        }
        
        public static string GetNextStarEmail(this ClaimsPrincipal principal)
        {
            var claim = principal?.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Email);
            return claim == null ? string.Empty : claim.Value;
        }
        
        public static string GetThirdPartyName(this ClaimsPrincipal principal)
        {
            var claim = principal?.Claims.FirstOrDefault(c => c.Type == NextStarClaimTypes.ThirdPartyName);
            return claim == null ? string.Empty : claim.Value;
        }
        public static string GetThirdPartyEmail(this ClaimsPrincipal principal)
        {
            var claim = principal?.Claims.FirstOrDefault(c => c.Type == NextStarClaimTypes.ThirdPartyEmail);
            return claim == null ? string.Empty : claim.Value;
        }
}