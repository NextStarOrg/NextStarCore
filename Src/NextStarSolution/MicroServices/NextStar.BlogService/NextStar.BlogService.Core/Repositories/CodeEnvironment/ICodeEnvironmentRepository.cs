using System.Linq.Expressions;
using NextStar.BlogService.Core.Entities.CodeEnvironment;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Repositories.CodeEnvironment;

public interface ICodeEnvironmentRepository
{
    Task<List<CommonSingleOutput>> SearchSingleAsync(string? searchText);
    IQueryable<BlogDbModels.CodeEnvironment> GetListQuery(Expression<Func<BlogDbModels.CodeEnvironment, bool>>? searchWhere);
    Task AddEntityAsync(CodeEnvironmentInput codeEnvironment);
    Task UpdateEntityAsync(CodeEnvironmentInput codeEnvironment);
    Task DeleteEntityAsync(Guid codeEnvironmentKey);
}