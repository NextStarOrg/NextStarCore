using NextStar.BlogService.Core.Entities.ArticleContent;

namespace NextStar.BlogService.Core.Repositories.ArticleContent;

public interface IArticleContentRepository
{
    Task<BlogDbModels.ArticleContent?> GetContentAsync(ArticleContentGetContentInput getContentInput);
    /// <summary>
    /// 返回的不携带content
    /// </summary>
    /// <returns></returns>
    Task<List<BlogDbModels.ArticleContent>> GetListAsync(int articleId);
    Task AddEntityAsync(ArticleContentAddInput addInput);
}