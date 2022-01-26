using NextStar.Library.AspNetCore.SessionDbModels;

namespace NextStar.Library.AspNetCore.Abstractions;

public interface INextStarSessionStore
{
    /// <summary>
    /// 判断session是否存在/是否没有过期
    /// </summary>
    /// <param name="sessionId"></param>
    /// <returns></returns>
    Task<bool> IsExistsOrNotExpiredAsync(Guid sessionId);

    /// <summary>
    /// 新创建session
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    Task CreateAsync(UserSession session);

    /// <summary>
    /// 删除缓存和数据中的
    /// </summary>
    /// <param name="sessionId"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid sessionId);

    Task<List<UserSession>> GetByUserIdAsync(Guid userId);
    Task<UserSession?> GetSessionByIdAsync(Guid sessionId);

    Task DeleteAllByUserIdAsync(Guid userId);
}