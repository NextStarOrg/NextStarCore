using System;
using System.Linq;
using IdentityModel;
using IdentityServer4;
using NextStar.Framework.Core.Consts;

namespace NextStar.IdentityServer.Extensions
{
    public static class IdentityServerUserExtensions
    {
        public static Guid? GetSessionId(this IdentityServerUser User)
        {
            var sessionIdClaim = User?.AdditionalClaims.FirstOrDefault(c => c.Type == NextStarClaimTypes.SessionId);
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

        public static string GetClientId(this IdentityServerUser User)
        {
            var clientIdClaim = User?.AdditionalClaims.FirstOrDefault(c => c.Type == JwtClaimTypes.ClientId);
            if (string.IsNullOrEmpty(clientIdClaim?.Value))
            {
                return null;
            }
            return clientIdClaim.Value;
        }

        public static string GetName(this IdentityServerUser User)
        {
            var nameClaim = User?.AdditionalClaims.FirstOrDefault(c => c.Type == JwtClaimTypes.Name);
            return nameClaim.Value;
        }

        public static string GetEmail(this IdentityServerUser User)
        {
            var nameClaim = User?.AdditionalClaims.FirstOrDefault(c => c.Type == JwtClaimTypes.Email);
            return nameClaim?.Value;
        }
        
        public static Guid? GetUserKey(this IdentityServerUser User)
        {
            var keyClaim = User?.SubjectId;
            if (string.IsNullOrEmpty(keyClaim))
            {
                return null;
            }
            Guid sId;
            if (!Guid.TryParse(keyClaim, out sId))
            {
                return null;
            }
            return sId;
        }
        
        public static long? GetPhone(this IdentityServerUser User)
        {
            var keyClaim =  User?.AdditionalClaims.FirstOrDefault(c => c.Type == NextStarClaimTypes.Phone)?.Value;
            if (string.IsNullOrEmpty(keyClaim))
            {
                return null;
            }
            long sId;
            if (!long.TryParse(keyClaim, out sId))
            {
                return null;
            }
            return sId;
        }
        
        public static NextStarLoginProvider? GetProvider(this IdentityServerUser User)
        {
            var keyClaim =  User?.AdditionalClaims.FirstOrDefault(c => c.Type == NextStarClaimTypes.Provider)?.Value;
            if (string.IsNullOrEmpty(keyClaim))
            {
                return null;
            }
            int sId;
            if (!int.TryParse(keyClaim, out sId))
            {
                return null;
            }
            return (NextStarLoginProvider)sId;
        }
    }
}