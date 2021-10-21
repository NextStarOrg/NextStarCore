using System;
using System.Threading.Tasks;
using NextStar.IdentityServer.NextStarAccountDbModels;

namespace NextStar.IdentityServer.Repositories.User
{
    public interface IUserRepository
    {
        Task<NextStarAccountDbModels.User> GetUserByKey(Guid key);
    }
}