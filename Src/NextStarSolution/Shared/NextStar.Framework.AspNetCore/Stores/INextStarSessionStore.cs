using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NextStar.Framework.AspNetCore.NextStarSessionDbModels;

namespace NextStar.Framework.AspNetCore.Stores
{
    public interface INextStarSessionStore
    {
        /// <summary>
        /// 判断session是否存在
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(Guid sessionId);

        /// <summary>
        /// 新创建session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        Task CreateAsync(UserSession session);

        /// <summary>
        /// 删除Session,及其派生的session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid sessionId);

        Task<List<UserSession>> GetByUserIdAsync(Guid userId);

        Task DeleteAllByUserIdAsync(Guid userId);
    }
}