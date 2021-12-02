using NextStar.Identity.AccountDbModels;

namespace NextStar.Identity.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByKey(Guid userKey);
}