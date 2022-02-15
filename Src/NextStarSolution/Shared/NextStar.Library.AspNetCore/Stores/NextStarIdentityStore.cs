using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.AspNetCore.Extensions;

namespace NextStar.Library.AspNetCore.Stores;

public class NextStarIdentityStore : INextStarIdentityStore
{
    private ClaimsPrincipal? Principal => _httpContextAccessor.HttpContext?.User;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public NextStarIdentityStore(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? SessionId => Principal?.GetNextStarSessionId();
    public Guid? UserKey => Principal?.GetNextStarUserKey();
    public string? Name => Principal?.GetNextStarName();
    public string? DisplayName => Principal?.GetNextStarDisplayName();
    public string? Email => Principal?.GetNextStarEmail();
    public string? ClientId => Principal?.GetNextStarClientId();
    public string? Provider => Principal?.GetNextStarProvider();
    public string? ThirdPartyEmail => Principal?.GetThirdPartyEmail();
    public string? ThirdPartyName => Principal?.GetThirdPartyEmail();

    public NextStarIdentityInfo IdentityInfo => new NextStarIdentityInfo()
    {
        SessionId = SessionId ?? Guid.Empty,
        UserKey = UserKey ?? Guid.Empty,
        Name = Name ?? string.Empty,
        DisplayName = DisplayName ?? string.Empty,
        Email = Email ?? string.Empty,
        ClientId = ClientId ?? string.Empty,
        Provider = Provider ?? string.Empty,
        ThirdPartyEmail = ThirdPartyEmail ?? string.Empty,
        ThirdPartyName = ThirdPartyName ?? string.Empty,
    };
}