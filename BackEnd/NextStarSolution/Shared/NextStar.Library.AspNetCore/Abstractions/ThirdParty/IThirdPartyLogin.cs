using NextStar.Library.Core.Consts;

namespace NextStar.Library.AspNetCore.Abstractions;

public interface IThirdPartyLogin
{
    Task<string> GetAuthorizationUrlAsync(string returnUrl, NextStarLoginType provider);

    Task<ThirdPartyLoginInfo> PostRequestTokenAsync(string state, string authorizationCode,
        NextStarLoginType provider);
}