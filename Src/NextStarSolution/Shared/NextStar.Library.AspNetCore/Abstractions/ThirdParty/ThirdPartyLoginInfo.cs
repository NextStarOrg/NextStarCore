using NextStar.Library.Core.Consts;

namespace NextStar.Library.AspNetCore.Abstractions;

public class ThirdPartyLoginInfo
{
    public string Key { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ReturnUrl { get; set; } = string.Empty;
    public NextStarLoginType Provider { get; set; } = NextStarLoginType.None;
}