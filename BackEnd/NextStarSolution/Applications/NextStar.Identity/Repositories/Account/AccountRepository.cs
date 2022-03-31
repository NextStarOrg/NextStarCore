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

    public async Task<User?> GetUserByKeyAsync(Guid userKey)
    {
        var user = await _nextStarDbContext.Users.Include(x=>x.UserProfile).AsNoTracking().FirstOrDefaultAsync(x => x.Key == userKey);
        return user;
    }

    public async Task<UserProfile?> GetUserProfileByLoginNameAsync(string loginName)
    {
        var userProfile = await _nextStarDbContext.UserProfiles.Include(x => x.UserKeyNavigation)
            .AsNoTracking().FirstOrDefaultAsync(x => x.LoginName.ToLower() == loginName.ToLower());
        return userProfile;
    }

    public async Task<Guid?> GetUserByThirdPartyKeyAsync(string key, NextStarLoginType provider)
    {
        var thirdParty = await _nextStarDbContext.UserThirdPartyLogins.AsNoTracking()
            .FirstOrDefaultAsync(x => x.ThirdPartyKey == key && x.LoginType == provider.ToString());
        if (thirdParty == null) return null;
        return thirdParty.UserKey;
    }

    public async Task CreateUserLoginHistoryAsync(UserLoginHistory userLoginHistory)
    {
        await _nextStarDbContext.UserLoginHistories.AddAsync(userLoginHistory);
        await _nextStarDbContext.SaveChangesAsync();
    }

    public async Task UpdateHistoryLogoutTimeAsync(Guid sessionId)
    {
        // 防止后续删除的sessionId再次出现，只取最新的登录时间
        var userHistory = await _nextStarDbContext.UserLoginHistories.OrderByDescending(x=>x.LoginTime).FirstOrDefaultAsync(x => x.SessionId == sessionId && !x.LogoutTime.HasValue);
        if (userHistory != null)
        {
            userHistory.LogoutTime = DateTime.Now;
            _nextStarDbContext.UserLoginHistories.Update(userHistory);
            await _nextStarDbContext.SaveChangesAsync();
        }
    }
}