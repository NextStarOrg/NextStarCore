using NextStar.BlogService.Core.NextStarBlogDbModels;

namespace NextStar.BlogService.Core.Repositories;

public interface IAuthorRepository
{
    IQueryable<Author> GetList();
    IQueryable<Author> GetListProfile();
    IQueryable<Author> GetListProfileByKey(Guid key);
    Task UpdateAuthorAsync(Author author);
}