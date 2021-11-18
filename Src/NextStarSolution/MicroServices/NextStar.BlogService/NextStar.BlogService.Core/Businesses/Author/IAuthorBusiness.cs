using NextStar.BlogService.Core.Entities;
using NextStar.BlogService.Core.NextStarBlogDbModels;
using NextStar.Framework.EntityFrameworkCore.Output;

namespace NextStar.BlogService.Core.Businesses;

public interface IAuthorBusiness
{
    Task<SelectListOutput<Author>> GetListAsync(AuthorSelectInput input);
}