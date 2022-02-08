using NextStar.Library.Core.Consts;

namespace NextStar.SystemService.Core.Entities.ThirdPartyLogin;

public class ThirdPartyLoginItem
{
    public int Id { get; set; }
    public Guid UserKey { get; set; }
    public string UserLoginName { get; set; } = string.Empty; 
    public string UserDisplayName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = null!;
    public NextStarLoginType LoginType { get; set; }
    public string ThirdPartyKey { get; set; } = null!;
    public string ThirdPartyName { get; set; } = string.Empty;
    public string ThirdPartyEmail { get; set; } = string.Empty;
}