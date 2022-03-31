namespace NextStar.Library.AspNetCore.Abstractions;

public class ThirdPartyLoginConfig
{
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string RedirectUri { get; set; } = null!;
    public string Scope { get; set; } = null!;
    public string OpenIdUri { get; set; } = null!;
    public string AuthorizationUri { get; set; } = null!;
    public string TokenUri { get; set; } = null!;
}