using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.AspNetCore.DbContexts;
using NextStar.Library.AspNetCore.SessionDbModels;
using NextStar.Library.Core.Abstractions;

namespace NextStar.Library.AspNetCore.Stores;

public class NextStarSessionStore:INextStarSessionStore
{
    private readonly SessionDbContext _context;
    private readonly ILogger<NextStarSessionStore> _logger;
    private readonly IDistributedCache<UserSession> _sessionCache;
    
    public NextStarSessionStore(SessionDbContext context,
        ILogger<NextStarSessionStore> logger,
        IDistributedCache<UserSession> sessionCache)
    {
        _context = context;
        _logger = logger;
        _sessionCache = sessionCache;
    } 
    
    public async Task<bool> IsExistsOrNotExpiredAsync(Guid sessionId)
    {
        //先判断缓存中是否存在
        var session = await _sessionCache.GetAsync(sessionId.ToString());
        if (session != null)
        {
            return DateTime.Compare(session.ExpiredTime, DateTime.Now.AddSeconds(30)) >= 0;
        }

        var sessionDb = await _context.UserSessions.FirstOrDefaultAsync(s => s.SessionId == sessionId);
        if (sessionDb != null)
        {
            // 判断时间是否过期
            if (DateTime.Compare(sessionDb.ExpiredTime,DateTime.Now.AddSeconds(30)) >= 0)
            {
                try
                {
                    await _sessionCache.SetAsync(sessionDb.SessionId.ToString(), sessionDb, new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4)
                    });
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Set {@Session} to cache occur error. ", sessionDb);
                    return false;
                }
            }
            await DeleteAsync(sessionId);
            return false;    
        }

        return false;
    }

    public async Task CreateAsync(UserSession session)
    {
        await _context.UserSessions.AddAsync(session);
        await _context.SaveChangesAsync();
        try
        {
            await _sessionCache.SetAsync(session.SessionId.ToString(), session, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Set {@Session} to cache occur error. ", session);
        }
    }

    public async Task DeleteAsync(Guid sessionId)
    {
        try
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(s => s.SessionId == sessionId);
            if (session != null)
            {
                _context.UserSessions.Remove(session);
                await _context.SaveChangesAsync();
            }

            await DeleteCacheAsync(sessionId);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "DbUpdateConcurrencyException occurred in DeleteAsync");
        }
    }

    public async Task<List<UserSession>> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserSessions.AsNoTracking().Where(u => u.UserKey == userId).ToListAsync();
    }
    
    public async Task<UserSession?> GetSessionByIdAsync(Guid sessionId)
    {
        return await _context.UserSessions.AsNoTracking().FirstOrDefaultAsync(u => u.SessionId == sessionId);
    }

    public async Task DeleteAllByUserIdAsync(Guid userId)
    {
        var sessions = await _context.UserSessions.Where(x => x.UserKey == userId).ToListAsync();
        var sessionIds = sessions.Select(x => x.SessionId).ToList();
        await DeleteCacheAsync(sessionIds);
        _context.UserSessions.RemoveRange(sessions);
        await _context.SaveChangesAsync();
    }
    
    
    #region Private Method

    private async Task DeleteCacheAsync(Guid sessionId)
    {
        try
        {
            await _sessionCache.RemoveAsync(sessionId.ToString());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Remove {SessionId} to cache occur error. ", sessionId);
        }
    }

    private async Task DeleteCacheAsync(List<Guid> sessionIds)
    {
        foreach (Guid sessionId in sessionIds)
        {
            await DeleteCacheAsync(sessionId);
        }
    }

    #endregion
}