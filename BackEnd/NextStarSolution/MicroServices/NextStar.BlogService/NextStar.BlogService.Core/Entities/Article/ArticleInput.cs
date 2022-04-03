namespace NextStar.BlogService.Core.Entities.Article;

public class ArticleInput
{
    public int ArticleId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? Category { get; set; } = null;
    public DateTime PublishTime { get; set; } = DateTime.Now;
}