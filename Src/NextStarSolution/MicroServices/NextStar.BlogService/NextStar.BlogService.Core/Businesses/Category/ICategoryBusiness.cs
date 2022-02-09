using NextStar.BlogService.Core.Entities.Category;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Businesses.Category;

public interface ICategoryBusiness
{
    Task<PageCommonDto<BlogDbModels.Category>> GetListAsync(CategorySelectInput selectInput);
    Task AddAsync(BlogDbModels.Category category);
}