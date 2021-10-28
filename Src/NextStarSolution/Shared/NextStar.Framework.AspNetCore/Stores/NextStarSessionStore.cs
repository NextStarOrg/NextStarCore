using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using NextStar.Framework.Abstractions.Cache;
using NextStar.Framework.AspNetCore.DbContexts;
using NextStar.Framework.AspNetCore.NextStarSessionDbModels;

namespace NextStar.Framework.AspNetCore.Stores
{
    public class NextStarSessionStore:INextStarSessionStore
    {
        private readonly NextStarSessionDbContext _context;
        private readonly ILogger<NextStarSessionStore> _logger;
        private readonly IDistributedCache<UserSession> _sessionCache;
        public NextStarSessionStore(NextStarSessionDbContext context,
            ILogger<NextStarSessionStore> logger,
            IDistributedCache<UserSession> sessionCache)
        {
            _context = context;
            _logger = logger;
            _sessionCache = sessionCache;
        } 
        
        
        public async Task<bool> IsExistsAsync(Guid sessionId)
        {
            //先判断缓存中是否存在
            var session = await _sessionCache.GetAsync(sessionId.ToString());
            if (session != null)
            {
                return true;
            }

            var sessionDb = await _context.UserSessions.FirstOrDefaultAsync(s => s.Id == sessionId);
            if (sessionDb != null)
            {
                // 判断时间是否过期
                if (sessionDb.ExpiredTime >= DateTime.Now)
                {
                    try
                    {
                        await _sessionCache.SetAsync(sessionDb.Id.ToString(), sessionDb, new DistributedCacheEntryOptions()
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4)
                        });
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Set {@session} to cache occur error. ", sessionDb);
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
                await _sessionCache.SetAsync(session.Id.ToString(), session, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Set {@session} to cache occur error. ", session);
            }
        }

        public async Task DeleteAsync(Guid sessionId)
        {
            try
            {
                var session = await _context.UserSessions.FirstOrDefaultAsync(s => s.Id == sessionId);
                if (session != null)
                {
                    _context.UserSessions.Remove(session);
                    await _context.SaveChangesAsync();
                }

                await DeleteCacheAsync(sessionId);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "DbUpdateConcurrencyException occurred in DeleteAsync.");
            }
        }

        public async Task<List<UserSession>> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserSessions.AsNoTracking().Where(u => u.UserKey == userId).ToListAsync();
        }

        public async Task DeleteAllByUserIdAsync(Guid userId)
        {
            var sessions = await _context.UserSessions.Where(x => x.UserKey == userId).ToListAsync();
            var sessionIds = sessions.Select(x => x.Id).ToList();
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
                _logger.LogError(ex, "Remove {sessionId} to cache occur error. ", sessionId);
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
}