using System.Text.Json.Serialization;

namespace NextStar.Library.AspNetCore.Abstractions;

public class ThirdPartyLoginTokenResult
{
    [JsonPropertyName("access_token")]
    [JsonInclude]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("expires_in")]
    [JsonInclude]
    public int ExpiresIn { get; set; } = 0;

    [JsonPropertyName("id_token")]
    [JsonInclude]
    public string IdToken { get; set; } = string.Empty;

    [JsonPropertyName("scope")]
    [JsonInclude]
    public string Scope { get; set; } = string.Empty;

    [JsonPropertyName("token_type")]
    [JsonInclude]
    public string TokenType { get; set; } = string.Empty;
}