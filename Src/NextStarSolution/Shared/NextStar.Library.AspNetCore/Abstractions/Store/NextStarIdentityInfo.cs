namespace NextStar.Library.AspNetCore.Abstractions;

public class NextStarIdentityInfo
{
    public Guid SessionId { get; set; }
    public Guid UserKey { get; set; }
    public string Name { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string Provider { get; set; } = null!;
    public string ThirdPartyEmail { get; set; } = null!;
    public string ThirdPartyName { get; set; } = null!;
}