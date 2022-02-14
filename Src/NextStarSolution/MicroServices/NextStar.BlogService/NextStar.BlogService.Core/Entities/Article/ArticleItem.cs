namespace NextStar.BlogService.Core.Entities.Article;

public class ArticleItem
{
    public int Id { get; set; }
    public Guid Key { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsPublish { get; set; }
    public DateTime PublishTime { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public List<ArticleTagItem> Tags { get; set; } = new List<ArticleTagItem>();
    public ArticleCategoryItem? Category { get; set; }
    public List<ArticleCodeEnvironmentItem> CodeEnvironment { get; set; } = new List<ArticleCodeEnvironmentItem>();
}

public class ArticleTagItem
{
    public Guid Key { get; set; }
    public string Name { get; set; } = null!;
    public string? BackgroundColor { get; set; }
    public string? TextColor { get; set; }
}

public class ArticleCategoryItem
{
    public Guid Key { get; set; }
    public string Name { get; set; } = null!;
}

public class ArticleCodeEnvironmentItem
{
    public Guid Key { get; set; }
    public string Name { get; set; } = null!;
    public string? IconUrl { get; set; }
    public string Version { get; set; } = null!;
}