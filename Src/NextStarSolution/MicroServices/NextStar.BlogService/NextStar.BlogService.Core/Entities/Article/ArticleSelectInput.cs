using NextStar.Library.MicroService.Inputs;

namespace NextStar.BlogService.Core.Entities.Article;

public class ArticleSelectInput : PageSearchTextInput
{
    public List<Guid> ClassKeys { get; set; } = new List<Guid>();
    public List<Guid> TagKeys  { get; set; } = new List<Guid>();
}