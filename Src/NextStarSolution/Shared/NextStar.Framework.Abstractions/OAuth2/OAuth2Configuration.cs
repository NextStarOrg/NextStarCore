using Newtonsoft.Json;

namespace NextStar.Framework.Abstractions.OAuth2
{
    public class OAuth2Configuration
    {
        public string ResponseType { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string GrantType { get; set; }
        public string Scope { get; set; }
    }
}