using System.Security.Claims;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using NextStar.Identity.NextStarDbModels;
using NextStar.Identity.Entities;
using NextStar.Library.AspNetCore.Abstractions;

namespace NextStar.Identity.Businesses;

public interface IAccountBusiness
{
    Task<UserProfile?> GetUserProfileByLoginNameAsync(string name);
    /// <summary>
    /// 验证用户是否已经登录
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<bool> ValidateUserIsAuthenticatedAsync(ClaimsPrincipal user);

    /// <summary>
    /// 构建认证服务的用户
    /// </summary>
    /// <param name="buildUserSessionDto"></param>
    /// <returns></returns>
    Task<IdentityServerUser?> BuildIdentityServerUserAsync(BuildUserSessionDto buildUserSessionDto);

    /// <summary>
    /// 设置身份认证属性
    /// </summary>
    /// <returns></returns>
    Task<AuthenticationProperties> GetAuthPropAsync();
}