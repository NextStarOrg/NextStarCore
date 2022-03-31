namespace NextStar.BlogService.Core.Entities.CodeEnvironment;

public class CodeEnvironmentInput
{
    public Guid Key { get; set; }
    public string Name { get; set; } = null!;
    public string? IconUrl { get; set; }
}