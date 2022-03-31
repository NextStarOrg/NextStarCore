using System.Text.Json.Serialization;

namespace NextStar.Library.AspNetCore.Abstractions;

public class ThirdPartyLoginOpenidConfiguration
{
    [JsonPropertyName("authorization_endpoint")]
    public string AuthorizationEndpoint { get; set; } = null!;
    [JsonPropertyName("token_endpoint")]
    public string TokenEndpoint { get; set; } = null!;
}