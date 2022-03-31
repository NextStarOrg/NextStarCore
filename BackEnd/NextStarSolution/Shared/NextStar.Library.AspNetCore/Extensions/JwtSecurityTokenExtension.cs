using System.IdentityModel.Tokens.Jwt;
using IdentityModel;

namespace NextStar.Library.AspNetCore.Extensions;

public static class JwtSecurityTokenExtension
{
    public static string GetEmail(this JwtSecurityToken securityToken)
    {
        var emailClaims = securityToken.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Email);
        return emailClaims != null ? emailClaims.Value : string.Empty;
    }

    public static string GetName(this JwtSecurityToken securityToken)
    {
        var nameClaims = securityToken.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name);
        return nameClaims != null ? nameClaims.Value : string.Empty;
    }
}