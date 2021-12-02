using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.AspNetCore.Extensions;
using NextStar.Library.Core.Abstractions;
using NextStar.Library.Core.Consts;

namespace NextStar.Library.AspNetCore.ThirdParty;

public class ThirdPartyLogin : IThirdPartyLogin
{
    private readonly IApplicationConfigStore _applicationConfigStore;
    private readonly ILogger<ThirdPartyLogin> _logger;
    private readonly IDistributedCache<ThirdPartyLoginStateCache> _stateCache;
    private readonly IDistributedCache<ThirdPartyLoginOpenidConfiguration> _openIdCache;
    private readonly IDistributedCache<ThirdPartyLoginConfig> _configCache;

    public ThirdPartyLogin(IApplicationConfigStore applicationConfigStore,
        ILogger<ThirdPartyLogin> logger,
        IDistributedCache<ThirdPartyLoginStateCache> stateCache,
        IDistributedCache<ThirdPartyLoginOpenidConfiguration> openIdCache,
        IDistributedCache<ThirdPartyLoginConfig> configCache)
    {
        _applicationConfigStore = applicationConfigStore;
        _logger = logger;
        _stateCache = stateCache;
        _openIdCache = openIdCache;
        _configCache = configCache;
    }

    /// <summary>
    /// 获取请求地址
    /// </summary>
    /// <param name="returnUrl"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public async Task<string> GetAuthorizationUrlAsync(string returnUrl, NextStarLoginType provider)
    {
        var config = await GetProviderConfigAsync(provider);
        var state = Guid.NewGuid().ToString("N");
        var cacheKey = state;
        var googleAuthorizationCodeRequestUrl =
            new GoogleAuthorizationCodeRequestUrl(new Uri(config.AuthorizationUri))
            {
                ResponseType = "code",
                Scope = config.Scope,
                ClientId = config.ClientId,
                State = state,
                RedirectUri = config.RedirectUri,
                Nonce = Guid.NewGuid().ToString("N")
            };

        await _stateCache.SetAsync(cacheKey, new ThirdPartyLoginStateCache()
        {
            State = state,
            ReturnUrl = returnUrl,
            Provider = provider.ToString()
        }, new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30)
        });

        return googleAuthorizationCodeRequestUrl.Build().ToString();
    }

    public async Task<ThirdPartyLoginInfo> PostRequestTokenAsync(string state, string authorizationCode,
        NextStarLoginType provider)
    {
        var config = await GetProviderConfigAsync(provider);
        var http = new HttpClient();
        var cache = await _stateCache.GetAsync(state);
        if (cache == null)
        {
            throw new NullReferenceException("office cache state is null");
        }

        var requestToken = new ThirdPartyLoginRequestTokenBody()
        {
            Code = authorizationCode,
            ClientId = config.ClientId,
            ClientSecret = config.ClientSecret,
            RedirectUri = config.RedirectUri,
            GrantType = "authorization_code"
        };

        var requestTokenStr = JsonSerializer.Serialize(requestToken);
        var json = JsonSerializer.Deserialize<Dictionary<string, string>>(requestTokenStr);
        var data = new FormUrlEncodedContent(json);
        var responseMessage = await http.PostAsync(new Uri(config.TokenUri), data);
        string result = await responseMessage.Content.ReadAsStringAsync();
        var oAuthTokenDto = JsonSerializer.Deserialize<ThirdPartyLoginTokenResult>(result);
        var idToken = new JwtSecurityToken(oAuthTokenDto.IdToken);
        var sub = idToken.Subject;
        var email = idToken.GetEmail();
        var name = idToken.GetName();
        await _stateCache.RemoveAsync(state);
        return new ThirdPartyLoginInfo()
        {
            Key = sub,
            Email = email,
            Name = name,
            ReturnUrl = cache.ReturnUrl,
        };
    }

    /// <summary>
    /// 获取第三方的配置
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    private async Task<ThirdPartyLoginConfig> GetProviderConfigAsync(NextStarLoginType provider)
    {
        var cache = await _configCache.GetAsync(provider.ToString());
        if (cache == null)
        {
            var openIdConfig = await GetProviderOpenIdConfigurationAsync(provider,
                _applicationConfigStore.GetConfigValue(provider + "LoginOpenIdUri"));
            var result = new ThirdPartyLoginConfig()
            {
                ClientId = _applicationConfigStore.GetConfigValue(provider + "LoginClientId"),
                ClientSecret = _applicationConfigStore.GetConfigValue(provider + "LoginClientSecret"),
                RedirectUri = _applicationConfigStore.GetConfigValue(provider + "LoginRedirectUri"),
                Scope = _applicationConfigStore.GetConfigValue(provider + "LoginScope"),
                OpenIdUri = _applicationConfigStore.GetConfigValue(provider + "LoginOpenIdUri"),
                AuthorizationUri = openIdConfig.AuthorizationEndpoint,
                TokenUri = openIdConfig.TokenEndpoint
            };
            await _configCache.SetAsync(provider.ToString(), result, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4)
            });
            return result;
        }

        return cache;
    }

    /// <summary>
    /// 获取满足OAuth2.0和OpenId Connect协议的配置端点
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="openIdUrl"></param>
    /// <returns></returns>
    private async Task<ThirdPartyLoginOpenidConfiguration> GetProviderOpenIdConfigurationAsync(
        NextStarLoginType provider,
        string openIdUrl)
    {
        var cache = await _openIdCache.GetAsync(provider.ToString());
        if (cache == null)
        {
            var http = new HttpClient();
            var result = await http.GetStringAsync(openIdUrl);
            var config = JsonSerializer.Deserialize<ThirdPartyLoginOpenidConfiguration>(result);
            await _openIdCache.SetAsync(provider.ToString(), config, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4)
            });
            return config;
        }

        return cache;
    }
}