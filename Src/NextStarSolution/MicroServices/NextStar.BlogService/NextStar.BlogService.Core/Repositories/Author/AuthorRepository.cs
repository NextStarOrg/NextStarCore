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

    public IQueryable<Author> GetList()
    {
        return _blogDbContext.Authors.AsQueryable();
    }

    public IQueryable<Author> GetListProfile()
    {
        return _blogDbContext.Authors.Include(x => x.AuthorProfile).AsQueryable();
    }

    public IQueryable<Author> GetListProfileByKey(Guid key)
    {
        return _blogDbContext.Authors.Include(x => x.AuthorProfile).Where(x=>x.Key == key).AsQueryable();
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        _blogDbContext.Authors.Update(author);
        await _blogDbContext.SaveChangesAsync();
    }
}