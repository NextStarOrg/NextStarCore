using System.Linq.Expressions;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Repositories.Tag;

public interface ITagRepository
{
    Task<List<CommonSingleOutput>> SearchSingleAsync(string? searchText);
    /// <summary>
    /// 检索列表
    /// </summary>
    /// <param name="searchWhere">为null则没有search检索</param>
    /// <returns></returns>
    IQueryable<BlogDbModels.Tag> GetListQuery(Expression<Func<BlogDbModels.Tag, bool>>? searchWhere);

    /// <summary>
    /// 添加实体内容
    /// </summary>
    /// <returns></returns>
    Task AddEntityAsync(BlogDbModels.Tag tag);

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <returns></returns>
    Task UpdateEntityAsync(BlogDbModels.Tag tag);

    /// <summary>
    /// 删除实体
    /// </summary>
    /// <returns></returns>
    Task DeleteEntityAsync(Guid tagKey);
}