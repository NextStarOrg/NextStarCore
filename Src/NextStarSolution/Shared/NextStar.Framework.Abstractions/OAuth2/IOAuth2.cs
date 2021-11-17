﻿using System.Threading.Tasks;
using NextStar.Framework.Core.Consts;

namespace NextStar.Framework.Abstractions.OAuth2
{
    public interface IOAuth2
    {
        Task<string> GetAuthorizationUrlAsync(string returnUrl,NextStarLoginProvider provider);
        Task<OAuthUserInfo> PostRequestTokenAsync(RequestTokenParameter tokenParameter);
    }
}