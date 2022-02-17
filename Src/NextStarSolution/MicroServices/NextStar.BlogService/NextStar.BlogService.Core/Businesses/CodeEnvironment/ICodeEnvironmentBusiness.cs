using NextStar.BlogService.Core.Entities.CodeEnvironment;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Businesses.CodeEnvironment;

public interface ICodeEnvironmentBusiness
{
    Task<List<CommonSingleOutput>> SearchSingleAsync(string? searchText);
    Task<PageCommonDto<BlogDbModels.CodeEnvironment>> GetListAsync(CodeEnvironmentSelectInput selectInput);
    Task AddAsync(CodeEnvironmentInput codeEnvironment);
    Task UpdateAsync(CodeEnvironmentInput codeEnvironment);
    Task DeleteAsync(Guid codeEnvironmentKey);
}