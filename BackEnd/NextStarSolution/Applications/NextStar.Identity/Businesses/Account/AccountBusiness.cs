﻿using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using NextStar.Identity.NextStarDbModels;
using NextStar.Identity.Entities;
using NextStar.Identity.Extensions;
using NextStar.Identity.Repositories;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.AspNetCore.Extensions;
using NextStar.Library.AspNetCore.SessionDbModels;
using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Businesses;

public class AccountBusiness:IAccountBusiness
{
    private readonly ILogger<AccountBusiness> _logger;
    private readonly INextStarSessionStore _nextStarSessionStore;
    private readonly IApplicationConfigStore _applicationConfigStore;
    private readonly IAccountRepository _repository;
    public AccountBusiness(IAccountRepository repository,
        ILogger<AccountBusiness> logger,
        INextStarSessionStore nextStarSessionStore,
        IApplicationConfigStore applicationConfigStore)
    {
        _repository = repository;
        _logger = logger;
        _nextStarSessionStore = nextStarSessionStore;
        _applicationConfigStore = applicationConfigStore;
    }

    public async Task<UserProfile?> GetUserProfileByLoginNameAsync(string name)
    {
        return await _repository.GetUserProfileByLoginNameAsync(name);
    }
    
    public async Task<bool> ValidateUserIsAuthenticatedAsync(ClaimsPrincipal user)
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
        var user = await _repository.GetUserByIdAsync(buildUserSessionDto.UserId);
        if (user == null) return null;
        var nextStarSessionId = Guid.NewGuid();
        var identityServerUser = new IdentityServerUser(user.Id.ToString());
        identityServerUser.AdditionalClaims.Add(new Claim(JwtClaimTypes.Name, user.UserProfile.LoginName));
        identityServerUser.AdditionalClaims.Add(new Claim(JwtClaimTypes.NickName, user.UserProfile.NickName));
        identityServerUser.AdditionalClaims.Add(new Claim(JwtClaimTypes.Email, user.Email));
        identityServerUser.AdditionalClaims.Add(new Claim(JwtClaimTypes.ClientId, buildUserSessionDto.ClientId));
        identityServerUser.AdditionalClaims.Add(new Claim(NextStarClaimTypes.SessionId, nextStarSessionId.ToString()));
        identityServerUser.AdditionalClaims.Add(new Claim(NextStarClaimTypes.Provider,
            buildUserSessionDto.Provider.ToString()));
        identityServerUser.AdditionalClaims.Add(new Claim(NextStarClaimTypes.ThirdPartyEmail,
            buildUserSessionDto.ThirdPartyEmail));
        identityServerUser.AdditionalClaims.Add(new Claim(NextStarClaimTypes.ThirdPartyName,
            buildUserSessionDto.ThirdPartyName));

        var session = new UserSession()
        {
            SessionId = nextStarSessionId,
            UserId = user.Id,
            CreatedTime = DateTime.Now,
            ExpiredTime = DateTime.Now.AddSeconds(buildUserSessionDto.Seconds).AddSeconds(10)
        };
        
        await _nextStarSessionStore.CreateAsync(session);
        return identityServerUser;
    }

    public async Task<AuthenticationProperties> GetAuthPropAsync()
    {
        var expiredSeconds =
            await _applicationConfigStore.GetConfigIntAsync(NextStarApplicationName.CookieExpiredSeconds);
        var props = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(expiredSeconds))
        };
        return props;
    }

}