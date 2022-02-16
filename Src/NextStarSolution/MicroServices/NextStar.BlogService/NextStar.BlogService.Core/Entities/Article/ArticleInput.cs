namespace NextStar.BlogService.Core.Entities.Article;

public class ArticleInput
{
    public Guid ArticleKey { get; set; } = Guid.Empty;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid? Category { get; set; } = null;
    public List<Guid> Tags { get; set; } = new List<Guid>();
    public List<Guid> CodeEnvironments { get; set; } = new List<Guid>();
    public bool IsPublish { get; set; } = false;
    public DateTime PublishTime { get; set; } = DateTime.Now;
}