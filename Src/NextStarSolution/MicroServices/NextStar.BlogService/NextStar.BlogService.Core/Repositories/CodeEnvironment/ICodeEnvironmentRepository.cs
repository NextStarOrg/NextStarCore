using System.Linq.Expressions;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Repositories.CodeEnvironment;

public interface ICodeEnvironmentRepository
{
    Task<List<CommonSingleOutput>> SearchSingleAsync(string searchText);
    IQueryable<BlogDbModels.CodeEnvironment> GetListQuery(Expression<Func<BlogDbModels.CodeEnvironment, bool>>? searchWhere);
    Task AddEntityAsync(BlogDbModels.CodeEnvironment codeEnvironment);
    Task UpdateEntityAsync(BlogDbModels.CodeEnvironment codeEnvironment);
    Task DeleteEntityAsync(Guid codeEnvironmentKey);
}