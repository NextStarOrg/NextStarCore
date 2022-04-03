using NextStar.Library.MicroService.Inputs;

namespace NextStar.BlogService.Core.Entities.ArticleContent;

public class ArticleContentSelectInput:PaginationInput
{
    public int ArticleId { get; set; }
}