using IdentityModel;
using IdentityServer4;
using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Extensions;

public static class IdentityServerUserExtensions
{
    public static Guid GetSessionId(this IdentityServerUser user)
    {
        var sessionIdClaim = user?.AdditionalClaims.FirstOrDefault(c => c.Type == NextStarClaimTypes.SessionId);
        if (string.IsNullOrEmpty(sessionIdClaim?.Value))
        {
            return Guid.Empty;
        }

        return !Guid.TryParse(sessionIdClaim.Value, out var sId) ? Guid.Empty : sId;
    }

    public static NextStarLoginType GetProvider(this IdentityServerUser user)
    {
        var providerClaim = user?.AdditionalClaims.FirstOrDefault(c => c.Type == NextStarClaimTypes.Provider);
        if (string.IsNullOrEmpty(providerClaim?.Value))
        {
            return NextStarLoginType.None;
        }

        var isEnum = Enum.TryParse<NextStarLoginType>(providerClaim.Value, true, out var provider);
        return isEnum ? provider : NextStarLoginType.None;
    }
}