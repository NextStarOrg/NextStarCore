using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextStar.IdentityServer.DbContexts;
using NextStar.IdentityServer.NextStarAccountDbModels;

namespace NextStar.IdentityServer.Repositories.User
{
    public class UserRepository:IUserRepository
    {
        private readonly NextStarAccountDbContext _accountDbContext;
        public UserRepository(NextStarAccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        public async Task<NextStarAccountDbModels.User> GetUserByKey(Guid key)
        {
            return await _accountDbContext.Users.Include(x=>x.UserProfile).FirstOrDefaultAsync(x => x.Key == key);
        }
    }
}