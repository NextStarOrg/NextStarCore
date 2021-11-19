namespace NextStar.BlogService.Core.Entities;

public class AuthorCreatInput
{
    public string Name { get; set; } = null!;
    public AuthorCreatProfileInput Profile { get; set; } = null!;
}

public class AuthorCreatProfileInput
{
    public string? Email { get; set; }
    public string? Url { get; set; }
}

