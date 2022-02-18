using NextStar.Identity.NextStarDbModels;
using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Repositories;

public interface IAccountRepository
{
    Task<User?> GetUserByKeyAsync(Guid userKey);
    Task<UserProfile?> GetUserProfileByLoginNameAsync(string loginName);
    Task<Guid?> GetUserByThirdPartyKeyAsync(string key, NextStarLoginType provider);
    Task CreateUserLoginHistoryAsync(UserLoginHistory userLoginHistory);
    Task UpdateHistoryLogoutTimeAsync(Guid sessionId);
}