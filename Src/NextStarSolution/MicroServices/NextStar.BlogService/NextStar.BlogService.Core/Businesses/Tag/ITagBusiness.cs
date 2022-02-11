using NextStar.BlogService.Core.Entities.Tag;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Businesses.Tag;

public interface ITagBusiness
{
    Task<PageCommonDto<BlogDbModels.Tag>> GetListAsync(TagSelectInput selectInput);
    Task AddAsync(BlogDbModels.Tag tag);
    Task UpdateAsync(BlogDbModels.Tag tag);
    Task DeleteAsync(Guid tagKey);
}