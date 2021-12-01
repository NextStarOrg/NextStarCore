namespace NextStar.Library.AspNetCore.Abstractions;

public class ThirdPartyLoginStateCache
{
    public string State { get; set; } = null!;
    public string ReturnUrl { get; set; } = string.Empty;
    public string Provider { get; set; } = null!;
}