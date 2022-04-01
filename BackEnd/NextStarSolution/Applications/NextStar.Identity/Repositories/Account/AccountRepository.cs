using Microsoft.EntityFrameworkCore;
using NextStar.Identity.NextStarDbModels;
using NextStar.Identity.DbContexts;
using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Repositories;

public class AccountRepository:IAccountRepository
{
    private readonly ILogger<AccountRepository> _logger;
    private readonly NextStarDbContext _nextStarDbContext;
    public AccountRepository(ILogger<AccountRepository> logger,
        NextStarDbContext nextStarDbContext)
    {
        _logger = logger;
        _nextStarDbContext = nextStarDbContext;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var user = await _nextStarDbContext.Users.Include(x=>x.UserProfile).AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
        return user;
    }

    public async Task<UserProfile?> GetUserProfileByLoginNameAsync(string loginName)
    {
        var userProfile = await _nextStarDbContext.UserProfiles.Include(x => x.User)
            .AsNoTracking().FirstOrDefaultAsync(x => x.LoginName.ToLower() == loginName.ToLower());
        return userProfile;
    }
}