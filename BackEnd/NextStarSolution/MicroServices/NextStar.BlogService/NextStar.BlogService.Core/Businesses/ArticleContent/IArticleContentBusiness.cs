using NextStar.BlogService.Core.Entities.ArticleContent;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Businesses.ArticleContent;

public interface IArticleContentBusiness
{
    Task<BlogDbModels.ArticleContent?> GetContentAsync(ArticleContentGetContentInput getContentInput);

    /// <summary>
    /// 返回的不携带content
    /// </summary>
    /// <returns></returns>
    Task<PageCommonDto<BlogDbModels.ArticleContent>> GetListAsync(ArticleContentSelectInput selectInput);

    Task AddAsync(ArticleContentAddInput addInput);
}