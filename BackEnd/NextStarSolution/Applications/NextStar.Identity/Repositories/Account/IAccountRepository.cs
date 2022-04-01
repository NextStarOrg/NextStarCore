using NextStar.Identity.NextStarDbModels;
using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Repositories;

public interface IAccountRepository
{
    Task<User?> GetUserByIdAsync(int userId);
    Task<UserProfile?> GetUserProfileByLoginNameAsync(string loginName);
}