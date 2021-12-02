using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Entities;

public class BuildUserSessionDto
{
    public Guid UserKey { get; set; } = Guid.Empty;
    public NextStarLoginType Provider { get; set; } = NextStarLoginType.None;
    public string ClientId { get; set; } = string.Empty;
    public string ThirdPartyEmail { get; set; } = string.Empty;
    public string ThirdPartyName { get; set; } = string.Empty;
}