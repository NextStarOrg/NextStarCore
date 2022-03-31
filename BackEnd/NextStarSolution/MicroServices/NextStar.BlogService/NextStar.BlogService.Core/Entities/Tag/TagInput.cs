namespace NextStar.BlogService.Core.Entities.Tag;

public class TagInput
{
    public Guid Key { get; set; }
    public string Name { get; set; } = null!;
    public string? BackgroundColor { get; set; }
    public string? TextColor { get; set; }
}