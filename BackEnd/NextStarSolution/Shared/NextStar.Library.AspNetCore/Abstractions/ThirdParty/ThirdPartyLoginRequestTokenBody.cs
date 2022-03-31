using System.Text.Json.Serialization;

namespace NextStar.Library.AspNetCore.Abstractions;

public class ThirdPartyLoginRequestTokenBody
{
    [JsonPropertyName("code")] public string Code { get; set; } = string.Empty;
    [JsonPropertyName("client_id")] public string ClientId { get; set; } = string.Empty;
    [JsonPropertyName("client_secret")] public string ClientSecret { get; set; } = string.Empty;
    [JsonPropertyName("redirect_uri")] public string RedirectUri { get; set; } = string.Empty;
    [JsonPropertyName("grant_type")] public string GrantType { get; set; } = string.Empty;
}