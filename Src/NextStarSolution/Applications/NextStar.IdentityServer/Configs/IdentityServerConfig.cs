using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;
using NextStar.Framework.EntityFrameworkCore.Input.Consts;

namespace NextStar.IdentityServer.Configs
{
    public static class IdentityServerConfig
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
            return resource;
        }

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("nextstarapi", "nextstar api",new List<string>(){
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    NextStarClaimTypes.SessionId
                })
            };

    }
}