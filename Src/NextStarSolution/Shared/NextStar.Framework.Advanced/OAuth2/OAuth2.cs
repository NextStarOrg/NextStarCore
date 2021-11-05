using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NextStar.Framework.Abstractions.Cache;
using NextStar.Framework.Abstractions.Config;
using NextStar.Framework.Abstractions.OAuth2;
using NextStar.Framework.Core.Consts;
using DistributedCacheEntryOptions = Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions;

namespace NextStar.Framework.Advanced.OAuth2
{
    public class OAuth2 : IOAuth2
    {
        private readonly ILogger<OAuth2> _logger;
        private readonly IDistributedCache<OAuthStateCache> _distributedCache;
        private readonly INextStarApplicationConfig _nextStarApplicationConfig;
        public OAuth2(ILogger<OAuth2> logger,
            IDistributedCache<OAuthStateCache> distributedCache,
            INextStarApplicationConfig nextStarApplicationConfig)
        {
            _logger = logger;
            _distributedCache = distributedCache;
            _nextStarApplicationConfig = nextStarApplicationConfig;
        }
        public async Task<string> GetAuthorizationUrlAsync(string returnUrl, NextStarLoginProvider provider)
        {
            var state = Guid.NewGuid().ToString("N");
            var cacheKey = state;
            var googleAuthorizationCodeRequestUrl =
                new GoogleAuthorizationCodeRequestUrl(new Uri(_requestAuthorizationUrl))
                {
                    ResponseType = "code",
                    Scope = _scope,
                    ClientId = _clientId,
                    State = state,
                    RedirectUri = _redirectUri,
                    Nonce = Guid.NewGuid().ToString("N")
                };
            
            await _distributedCache.SetAsync(cacheKey, new OfficeStateCache()
            {
                State = state,
                ReturnUrl = returnUrl
            }, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30)
            });
            
            return googleAuthorizationCodeRequestUrl.Build().ToString();
        }

        public async Task<OAuthUserInfo> PostRequestTokenAsync(RequestTokenParameter tokenParameter)
        {
            throw new System.NotImplementedException();
        }


        private async Task<OpenIdConfiguration> GetProviderConfigurationAsync(NextStarLoginProvider provider)
        {
            var openIdUri = provider switch
            {
                NextStarLoginProvider.Microsoft => _nextStarApplicationConfig.GetConfigValue(NextStarApplicationName
                    .MicrosoftLoginProvider.MicrosoftLoginOpenIdUri),
                NextStarLoginProvider.Google => _nextStarApplicationConfig.GetConfigValue(NextStarApplicationName
                    .GoogleLoginProvider.GoogleLoginOpenIdUri),
                _ => throw new ArgumentOutOfRangeException(nameof(provider), provider, null)
            };
            var http = new HttpClient();
            var responseMessage = await http.GetAsync(new Uri(openIdUri));
            var result = await responseMessage.Content.ReadAsStringAsync();
            var openIdConfiguration = JsonConvert.DeserializeObject<OpenIdConfiguration>(result);
            return openIdConfiguration;
        }

        private async Task<OAuth2Configuration> GetProviderConfigAsync(NextStarLoginProvider provider)
        {
            var config = new OAuth2Configuration();
            switch (provider)
            {
                case NextStarLoginProvider.Microsoft:
                    config.ClientId =
                        await _nextStarApplicationConfig.GetConfigValueAsync(NextStarApplicationName
                            .MicrosoftLoginProvider.MicrosoftLoginClientId);
                    config.ClientSecret =
                        await _nextStarApplicationConfig.GetConfigValueAsync(NextStarApplicationName
                            .MicrosoftLoginProvider.MicrosoftLoginClientSecret);
                    config.RedirectUri =
                        await _nextStarApplicationConfig.GetConfigValueAsync(NextStarApplicationName
                            .MicrosoftLoginProvider.MicrosoftLoginRedirectUri);
                    break;
                case NextStarLoginProvider.Google:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(provider), provider, null);
            }
        }
    }
}