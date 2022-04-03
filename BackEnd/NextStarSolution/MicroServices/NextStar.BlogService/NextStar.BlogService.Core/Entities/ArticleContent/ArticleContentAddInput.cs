namespace NextStar.BlogService.Core.Entities.ArticleContent;

public class ArticleContentAddInput
{
    public int ArticleId { get; set; }
    public string Content { get; set; } = null!;
    public string? CommitMessage { get; set; }
}