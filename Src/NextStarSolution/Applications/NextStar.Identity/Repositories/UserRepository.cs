using Microsoft.EntityFrameworkCore;
using NextStar.Identity.AccountDbModels;
using NextStar.Identity.DbContexts;

namespace NextStar.Identity.Repositories;

public class UserRepository:IUserRepository
{
    private readonly ILogger<UserRepository> _logger;
    private readonly AccountDbContext _accountDbContext;
    public UserRepository(ILogger<UserRepository> logger,
        AccountDbContext accountDbContext)
    {
        _logger = logger;
        _accountDbContext = accountDbContext;
    }

    public async Task<User?> GetUserByKey(Guid userKey)
    {
        var user = await _accountDbContext.Users.Include(x=>x.UserProfile).FirstOrDefaultAsync(x => x.Key == userKey);
        return user;
    }
}