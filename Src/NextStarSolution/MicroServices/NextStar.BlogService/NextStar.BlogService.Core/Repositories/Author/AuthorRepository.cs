using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NextStar.BlogService.Core.DbContexts;
using NextStar.BlogService.Core.NextStarBlogDbModels;

namespace NextStar.BlogService.Core.Repositories;

public class AuthorRepository:IAuthorRepository
{
    private readonly ILogger<AuthorRepository> _logger;
    private readonly BlogDbContext _blogDbContext;
    public AuthorRepository(ILogger<AuthorRepository> logger,
        BlogDbContext blogDbContext)
    {
        _logger = logger;
        _blogDbContext = blogDbContext;
    }

    public IQueryable<Author> GetAuthors()
    {
        return _blogDbContext.Authors.AsQueryable();
    }

    public IQueryable<Author> GetAuthorsWithProfile()
    {
        return _blogDbContext.Authors.Include(x => x.AuthorProfile).AsQueryable();
    }

    public async Task CreateAsync(Author author)
    {
        await _blogDbContext.Authors.AddAsync(author);
        await _blogDbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAuthorAsync(Author author)
    {
        _blogDbContext.Authors.Update(author);
        await _blogDbContext.SaveChangesAsync();
    }

    public async Task<Author?> GetWithProfileByKeyAsync(Guid key)
    {
        return await _blogDbContext.Authors.Include(x=>x.AuthorProfile).FirstOrDefaultAsync(x => x.Key == key);
    }

    public async Task<bool> IsExistAsync(Guid key)
    {
        return await _blogDbContext.Authors.AnyAsync(x => x.Key == key);
    }
}