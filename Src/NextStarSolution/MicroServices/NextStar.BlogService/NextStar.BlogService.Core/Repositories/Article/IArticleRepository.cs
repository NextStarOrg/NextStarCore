using NextStar.BlogService.Core.Entities.Article;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Repositories.Article;

public interface IArticleRepository
{
    Task<List<CommonSingleOutput>> SearchSingleAsync(string searchText);
    Task<IQueryable<BlogDbModels.Article>> SelectEntityAsync();
    Task<bool> AddEntityAsync(ArticleInput articleInput);
    Task<bool> UpdateEntityAsync(ArticleInput articleInput);
    Task DeleteEntityAsync(Guid articleKey);
    /// <summary>
    /// 更新文章关联的分类
    /// </summary>
    /// <param name="articleItems"></param>
    /// <returns></returns>
    Task GetArticleCategoryByArticles(List<ArticleItem> articleItems);

    Task GetArticleTagByArticles(List<ArticleItem> articleItems);

    Task GetArticleCodeEnvironmentByArticles(List<ArticleItem> articleItems);
}