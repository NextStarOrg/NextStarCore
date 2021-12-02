using System.Security.Claims;
using IdentityServer4;
using NextStar.Identity.Entities;

namespace NextStar.Identity.Businesses;

public interface ICommonBusiness
{
    /// <summary>
    /// 验证用户是否已经登录
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<bool> ValidateUserIsAuthenticated(ClaimsPrincipal user);

    /// <summary>
    /// 构建认证服务的用户
    /// </summary>
    /// <param name="buildUserSessionDto"></param>
    /// <returns></returns>
    Task<IdentityServerUser?> BuildIdentityServerUserAsync(BuildUserSessionDto buildUserSessionDto);
}