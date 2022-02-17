using NextStar.BlogService.Core.Entities.Article;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Businesses.Article;

public interface IArticleBusiness
{
    Task<List<CommonSingleOutput>> SearchSingleAsync(string? searchText);
    Task<PageCommonDto<ArticleItem>> SelectArticleAsync(ArticleSelectInput selectInput);
    Task AddAsync(ArticleInput articleInput);
    Task UpdateAsync(ArticleInput articleInput);
    Task DeleteAsync(Guid articleKey);
}