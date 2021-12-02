using IdentityModel;
using IdentityServer4.Models;
using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Configs;

public class IdentityServerConfig
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            CreateProfileResource(),
        };

    private static IdentityResource CreateProfileResource()
    {
        var resource = new IdentityResources.Profile();
        resource.UserClaims.Add(JwtClaimTypes.Email);
        resource.UserClaims.Add(JwtClaimTypes.Name);
        resource.UserClaims.Add(NextStarClaimTypes.SessionId);
        resource.UserClaims.Add(NextStarClaimTypes.ThirdPartyName);
        resource.UserClaims.Add(NextStarClaimTypes.ThirdPartyEmail);
        resource.UserClaims.Add(NextStarClaimTypes.Provider);
        return resource;
    }

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("nextstarapi", "nextstar api",new List<string>(){
                JwtClaimTypes.Email,
                JwtClaimTypes.Name,
                NextStarClaimTypes.SessionId,
                NextStarClaimTypes.ThirdPartyName,
                NextStarClaimTypes.ThirdPartyEmail,
                NextStarClaimTypes.Provider
            })
        };

}