namespace NextStar.BlogService.Core.Entities.Article;

public class ArticleItem
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime PublishTime { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public ArticleCategoryItem? Category { get; set; }
}
public class ArticleCategoryItem
{
    public Guid Key { get; set; }
    public string Name { get; set; } = null!;
}
