using NextStar.BlogService.Core.Entities.Category;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Businesses.Category;

public interface ICategoryBusiness
{
    Task<List<CommonSingleOutput>> SearchSingleAsync(string? searchText);
    Task<PageCommonDto<BlogDbModels.Category>> GetListAsync(CategorySelectInput selectInput);
    Task AddAsync(CategoryInput category);
    Task UpdateAsync(CategoryInput category);
    Task DeleteAsync(int categoryKey);
}