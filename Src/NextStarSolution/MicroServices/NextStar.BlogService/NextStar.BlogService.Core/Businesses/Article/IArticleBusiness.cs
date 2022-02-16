using NextStar.BlogService.Core.Entities.Article;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Businesses.Article;

public interface IArticleBusiness
{
    Task<PageCommonDto<ArticleItem>> SelectArticleAsync(ArticleSelectInput selectInput);
}