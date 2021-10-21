using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using NextStar.Framework.Abstractions.Config;
using NextStar.Framework.AspNetCore.Extensions;
using NextStar.Framework.AspNetCore.NextStarSessionDbModels;
using NextStar.Framework.AspNetCore.Stores;
using NextStar.Framework.Core.Consts;
using NextStar.IdentityServer.Dto;
using NextStar.IdentityServer.Extensions;
using NextStar.IdentityServer.Repositories.User;

namespace NextStar.IdentityServer.Businesses
{
    public class CommonBusiness:ICommonBusiness
    {
        private readonly INextStarSessionStore _nextStarSessionStore;
        private readonly IUserRepository _userRepository;
        private readonly INextStarApplicationConfig _nextStarApplicationConfig;

        public CommonBusiness(INextStarSessionStore nextStarSessionStore,
            IUserRepository userRepository,
            INextStarApplicationConfig nextStarApplicationConfig)
        {
            _nextStarSessionStore = nextStarSessionStore;
            _userRepository = userRepository;
            _nextStarApplicationConfig = nextStarApplicationConfig;
        }

        public async Task<bool> ValidateUserAuthenticated(ClaimsPrincipal user)
        {
            if (user is { Identity: { IsAuthenticated: true } })
            {
                var sessionId = user.GetNextStarSessionId();
                // SessionId存在 并且不在数据库中存在 则强制退出
                if (sessionId != null && !await _nextStarSessionStore.IsExistsAsync(sessionId.Value))
                {
                    return false;
                }

                return true;
            }
            return false;
        }
        
        public async Task<(IdentityServerUser User,AuthenticationProperties Props)> BuildIdentityServerUserAsync(BuildUserSessionDto buildUserSessionDto)
        {
            var user = await _userRepository.GetUserByKey(buildUserSessionDto.UserKey);
            Guid leyserSessionId = Guid.NewGuid();
            var identityServerUser = new IdentityServerUser(user.Key.ToString());
            identityServerUser.AdditionalClaims.Add(new Claim(JwtClaimTypes.Name, user.UserProfile.NickName));
            identityServerUser.AdditionalClaims.Add(new Claim(JwtClaimTypes.Email, user.UserProfile.Email));
            identityServerUser.AdditionalClaims.Add(new Claim(NextStarClaimTypes.Phone, user.UserProfile.Phone.ToString()));
            identityServerUser.AdditionalClaims.Add(new Claim(JwtClaimTypes.ClientId, buildUserSessionDto.ClientId));
            identityServerUser.AdditionalClaims.Add(new Claim(NextStarClaimTypes.SessionId, leyserSessionId.ToString()));
            identityServerUser.AdditionalClaims.Add(new Claim(NextStarClaimTypes.Provider, ((int)buildUserSessionDto.LoginProvider).ToString()));

            var session = new UserSession()
            {
                UserKey = buildUserSessionDto.UserKey,
                Provider = (int)buildUserSessionDto.LoginProvider,
                Phone = user.UserProfile.Phone,
                Name = user.UserProfile.NickName,
                Email = user.UserProfile.Email,
                CreatedTime = DateTime.Now,
                ExpiredTime = DateTime.Now.AddSeconds(buildUserSessionDto.Seconds)
            };

            await _nextStarSessionStore.CreateAsync(session);
            
            var expiredSeconds =
                _nextStarApplicationConfig.GetConfigIntValue(NextStarApplicationName.CookieExpiredSeconds);
            AuthenticationProperties props = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(expiredSeconds))
            };
            return (identityServerUser, props);
        }
    }
}