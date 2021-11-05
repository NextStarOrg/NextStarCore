using Newtonsoft.Json;

namespace NextStar.Framework.Abstractions.OAuth2
{
    public class OpenIdConfiguration
    {
        [JsonProperty]
        public string Issuer { get; set; }
        [JsonProperty("authorization_endpoint")]
        public string AuthorizationEndpoint { get; set; }
        [JsonProperty("token_endpoint")]
        public string TokenEndpoint { get; set; }
        [JsonProperty("userinfo_endpoint")]
        public string UserinfoEndpoint { get; set; }
    }
}