using NextStar.Identity.AccountDbModels;
using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Repositories;

public interface IAccountRepository
{
    Task<User?> GetUserByKey(Guid userKey);
    Task<UserProfile?> GetUserProfileByLoginName(string loginName);
    Task<Guid?> GetUserByThirdPartyKey(string key, NextStarLoginType provider);
}