using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using NextStar.Framework.EntityFrameworkCore.Input.Consts;

namespace NextStar.Framework.AspNetCore.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        // <summary>
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
        
        public static long? GetNextStarPhone(this ClaimsPrincipal principal)
        {
            var claim = principal?.Claims.FirstOrDefault(c => c.Type == NextStarClaimTypes.Phone);
            if (string.IsNullOrEmpty(claim?.Value))
            {
                return null;
            }
            long sId;
            if (!long.TryParse(claim.Value, out sId))
            {
                return null;
            }
            return sId;
        }
    }
}