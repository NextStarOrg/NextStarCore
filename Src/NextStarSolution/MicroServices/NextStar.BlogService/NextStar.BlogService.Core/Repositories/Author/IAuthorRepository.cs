using NextStar.BlogService.Core.NextStarBlogDbModels;

namespace NextStar.BlogService.Core.Repositories;

public interface IAuthorRepository
{
    IQueryable<Author> GetAuthors();
    IQueryable<Author> GetAuthorsWithProfile();
    Task CreateAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task<Author?> GetWithProfileByKeyAsync(Guid key);
    Task<bool> IsExistAsync(Guid key);
}