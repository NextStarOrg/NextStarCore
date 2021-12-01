using System.Text.Json.Serialization;

namespace NextStar.Library.AspNetCore.Abstractions;

public class ThirdPartyLoginTokenResult
{
    [JsonPropertyName("access_token")] [JsonInclude] public string AccessToken { get; set; }
    [JsonPropertyName("expires_in")] [JsonInclude] public int ExpiresIn { get; set; }
    [JsonPropertyName("id_token")] [JsonInclude] public string IdToken { get; set; }
    [JsonPropertyName("scope")] [JsonInclude] public string Scope { get; set; }
    [JsonPropertyName("token_type")] [JsonInclude] public string TokenType { get; set; }
}