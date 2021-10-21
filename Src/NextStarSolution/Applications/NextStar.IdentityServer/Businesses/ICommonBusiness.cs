using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using NextStar.IdentityServer.Dto;

namespace NextStar.IdentityServer.Businesses
{
    public interface ICommonBusiness
    {
        Task<bool> ValidateUserAuthenticated(ClaimsPrincipal user);

        Task<(IdentityServerUser User, AuthenticationProperties Props)> BuildIdentityServerUserAsync(
            BuildUserSessionDto buildUserSessionDto);
    }
}