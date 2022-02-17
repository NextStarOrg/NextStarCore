using NextStar.BlogService.Core.Entities.Tag;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Businesses.Tag;

public interface ITagBusiness
{
    Task<List<CommonSingleOutput>> SearchSingleAsync(string? searchText);
    Task<PageCommonDto<BlogDbModels.Tag>> GetListAsync(TagSelectInput selectInput);
    Task AddAsync(TagInput tag);
    Task UpdateAsync(TagInput tag);
    Task DeleteAsync(Guid tagKey);
}