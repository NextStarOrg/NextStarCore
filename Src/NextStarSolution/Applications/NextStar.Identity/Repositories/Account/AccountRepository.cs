using Microsoft.EntityFrameworkCore;
using NextStar.Identity.AccountDbModels;
using NextStar.Identity.DbContexts;
using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Repositories;

public class AccountRepository:IAccountRepository
{
    private readonly ILogger<AccountRepository> _logger;
    private readonly AccountDbContext _accountDbContext;
    public AccountRepository(ILogger<AccountRepository> logger,
        AccountDbContext accountDbContext)
    {
        _logger = logger;
        _accountDbContext = accountDbContext;
    }

    public async Task<User?> GetUserByKey(Guid userKey)
    {
        var user = await _accountDbContext.Users.Include(x=>x.UserProfile).AsNoTracking().FirstOrDefaultAsync(x => x.Key == userKey);
        return user;
    }

    public async Task<UserProfile?> GetUserProfileByLoginName(string loginName)
    {
        var userProfile = await _accountDbContext.UserProfiles.Include(x => x.UserKeyNavigation)
            .AsNoTracking().FirstOrDefaultAsync(x => x.LoginName.ToLower() == loginName.ToLower());
        return userProfile;
    }

    public async Task<Guid?> GetUserByThirdPartyKey(string key, NextStarLoginType provider)
    {
        var thirdParty = await _accountDbContext.UserThirdPartyLogins.AsNoTracking()
            .FirstOrDefaultAsync(x => x.ThirdPartyKey == key && x.LoginType == provider.ToString());
        if (thirdParty == null) return null;
        return thirdParty.UserKey;
    }
}