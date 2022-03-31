namespace NextStar.Library.AspNetCore.Abstractions;

public interface INextStarIdentityStore
{
    Guid? SessionId { get; }
    Guid? UserKey { get; }
    string? Name { get; }
    string? DisplayName { get; }
    string? Email { get; }
    string? ClientId { get; }
    string? Provider { get; }
    string? ThirdPartyEmail { get; }
    string? ThirdPartyName { get; }
    /// <summary>
    /// 使用此数据，必须保证登录着
    /// </summary>
    NextStarIdentityInfo IdentityInfo { get; }
}