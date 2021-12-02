using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using NextStar.Identity.Entities;
using NextStar.Identity.Repositories;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.AspNetCore.Extensions;
using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Businesses;

public class CommonBusiness : ICommonBusiness
{
    private readonly ILogger<CommonBusiness> _logger;
    private readonly INextStarSessionStore _nextStarSessionStore;
    private readonly IUserRepository _userRepository;
    private readonly IApplicationConfigStore _applicationConfigStore;

    public CommonBusiness(ILogger<CommonBusiness> logger,
        INextStarSessionStore nextStarSessionStore,
        IUserRepository userRepository,
        IApplicationConfigStore applicationConfigStore)
    {
        _logger = logger;
        _nextStarSessionStore = nextStarSessionStore;
        _userRepository = userRepository;
        _applicationConfigStore = applicationConfigStore;
    }

    public async Task<bool> ValidateUserIsAuthenticated(ClaimsPrincipal user)
    {
        if (user is { Identity: { IsAuthenticated: true } })
        {
            var sessionId = user.GetNextStarSessionId();
            // SessionId存在 并且不在数据库中存在 则强制退出
            if (sessionId != null && await _nextStarSessionStore.IsExistsOrNotExpiredAsync(sessionId.Value))
            {
                return true;
            }

            return false;
        }

        return false;
    }

    public async Task<IdentityServerUser?> BuildIdentityServerUserAsync(BuildUserSessionDto buildUserSessionDto)
    {
        var user = await _userRepository.GetUserByKey(Guid.Empty);
        if (user == null) return null;
        var nextStarSessionId = Guid.NewGuid();
        var identityServerUser = new IdentityServerUser(user.Key.ToString());
        identityServerUser.AdditionalClaims.Add(new Claim(JwtClaimTypes.Name, user.UserProfile.DisplayName));
        identityServerUser.AdditionalClaims.Add(new Claim(JwtClaimTypes.Email, user.Email));
        identityServerUser.AdditionalClaims.Add(new Claim(JwtClaimTypes.ClientId, buildUserSessionDto.ClientId));
        identityServerUser.AdditionalClaims.Add(new Claim(NextStarClaimTypes.SessionId, nextStarSessionId.ToString()));
        identityServerUser.AdditionalClaims.Add(new Claim(NextStarClaimTypes.Provider,
            buildUserSessionDto.Provider.ToString()));
        identityServerUser.AdditionalClaims.Add(new Claim(NextStarClaimTypes.ThirdPartyEmail,
            buildUserSessionDto.ThirdPartyEmail));
        identityServerUser.AdditionalClaims.Add(new Claim(NextStarClaimTypes.ThirdPartyName,
            buildUserSessionDto.ThirdPartyName));
        return identityServerUser;
    }
}