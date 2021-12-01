using System.Text.Json.Serialization;

namespace NextStar.Library.AspNetCore.Abstractions;

public class ThirdPartyLoginRequestTokenBody
{
    [JsonPropertyName("code")] public string Code { get; set; }
    [JsonPropertyName("client_id")] public string ClientId { get; set; }
    [JsonPropertyName("client_secret")] public string ClientSecret { get; set; }
    [JsonPropertyName("redirect_uri")] public string RedirectUri { get; set; }
    [JsonPropertyName("grant_type")] public string GrantType { get; set; }
}