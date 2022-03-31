namespace NextStar.BlogService.Core.Entities.Category;

public class CategoryInput
{
    public Guid Key { get; set; }
    public string Name { get; set; } = null!;
}