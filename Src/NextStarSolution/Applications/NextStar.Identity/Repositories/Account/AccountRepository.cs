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

    public async Task<User?> GetUserByKeyAsync(Guid userKey)
    {
        var user = await _accountDbContext.Users.Include(x=>x.UserProfile).AsNoTracking().FirstOrDefaultAsync(x => x.Key == userKey);
        return user;
    }

    public async Task<UserProfile?> GetUserProfileByLoginNameAsync(string loginName)
    {
        var userProfile = await _accountDbContext.UserProfiles.Include(x => x.UserKeyNavigation)
            .AsNoTracking().FirstOrDefaultAsync(x => x.LoginName.ToLower() == loginName.ToLower());
        return userProfile;
    }

    public async Task<Guid?> GetUserByThirdPartyKeyAsync(string key, NextStarLoginType provider)
    {
        var thirdParty = await _accountDbContext.UserThirdPartyLogins.AsNoTracking()
            .FirstOrDefaultAsync(x => x.ThirdPartyKey == key && x.LoginType == provider.ToString());
        if (thirdParty == null) return null;
        return thirdParty.UserKey;
    }

    public async Task CreateUserLoginHistoryAsync(UserLoginHistory userLoginHistory)
    {
        await _accountDbContext.UserLoginHistories.AddAsync(userLoginHistory);
        await _accountDbContext.SaveChangesAsync();
    }

    public async Task UpdateHistoryLogoutTimeAsync(Guid sessionId)
    {
        // 防止后续删除的sessionId再次出现，只取最新的登录时间
        var userHistory = await _accountDbContext.UserLoginHistories.OrderByDescending(x=>x.LoginTime).FirstOrDefaultAsync(x => x.SessionId == sessionId && !x.LogoutTime.HasValue);
        if (userHistory != null)
        {
            userHistory.LogoutTime = DateTime.UtcNow;
            _accountDbContext.UserLoginHistories.Update(userHistory);
            await _accountDbContext.SaveChangesAsync();
        }
    }
}