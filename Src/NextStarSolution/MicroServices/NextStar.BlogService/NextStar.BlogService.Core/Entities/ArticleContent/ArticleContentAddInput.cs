namespace NextStar.BlogService.Core.Entities.ArticleContent;

public class ArticleContentAddInput
{
    public Guid ArticleKey { get; set; }
    public string Content { get; set; } = null!;
    public string? CommitMessage { get; set; }
}