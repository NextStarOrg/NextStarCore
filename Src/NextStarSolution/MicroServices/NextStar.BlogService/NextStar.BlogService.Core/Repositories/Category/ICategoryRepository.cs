using System.Linq.Expressions;
using NextStar.BlogService.Core.Entities.Category;

namespace NextStar.BlogService.Core.Repositories.Category;

public interface ICategoryRepository
{
    /// <summary>
    /// 检索列表
    /// </summary>
    /// <param name="searchWhere">为null则没有search检索</param>
    /// <returns></returns>
    IQueryable<BlogDbModels.Category> GetListQuery(Expression<Func<BlogDbModels.Category, bool>>? searchWhere);

    /// <summary>
    /// 添加实体内容
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    Task AddEntityAsync(BlogDbModels.Category category);
}