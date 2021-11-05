using NextStar.Framework.Core.Consts;

namespace NextStar.Framework.Abstractions.OAuth2
{
    public class OAuthStateCache
    {
        public NextStarLoginProvider Provider { get; set; }
        public string State { get; set; }
        public string ReturnUrl { get; set; }
    }
}